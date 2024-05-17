namespace GreenLibrary.Server.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Text.Json;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;

    using GreenLibrary.Data.Entities;
    using GreenLibrary.Extensions;
    using GreenLibrary.Services.Dtos.User;
    using GreenLibrary.Services.Interfaces;

    using static GreenLibrary.Common.ApplicationConstants;
    using static GreenLibrary.Common.ErrorMessages.ArticleErrorMessages;
    using static GreenLibrary.Common.ErrorMessages.UserErrorMessages;
    using static GreenLibrary.Common.SuccessfulMessage.UserSuccessfulMessages;

    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IUserService userService;
        private readonly IImageService imageService;
        private readonly IArticleService articleService;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration config, IUserService userService, IImageService imageService, IArticleService articleService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            configuration = config;
            this.userService = userService;
            this.imageService = imageService;
            this.articleService = articleService;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByNameAsync(loginDto.Username);

            if (user != null && user.IsDeleted == false)
            {
                var result = await signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);

                if (result.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(user!);

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email!),
                        new Claim(ClaimTypes.Role, roles[0]),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));
                    SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                    var securityToken = new JwtSecurityToken(
                        claims: claims,
                        expires: DateTime.Now.AddMinutes(1),
                        issuer: configuration.GetSection("Jwt:Issuer").Value,
                        audience: configuration.GetSection("Jwt:Audience").Value,
                        signingCredentials: signingCred);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                    return Ok(new
                    {
                        token = tokenString,
                        username = user.UserName,
                        roles = roles[0]
                    });
                }
            }

            return Unauthorized(InvalidUsernameOrPassword);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
            {
                return BadRequest(EmailAlreadyExist);
            }

            var username = await userManager.FindByNameAsync(registerDto.UserName);
            if (username != null)
            {
                return BadRequest(UserNameAlreadyExist);
            }

            if (registerDto.Password != registerDto.RepeatPassword)
            {
                return BadRequest(PasswordDoesntMatch);
            }

            user = new User()
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            await userManager.SetEmailAsync(user, registerDto.Email);
            await userManager.SetUserNameAsync(user, registerDto.UserName);

            var result = await userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            await userManager.AddToRoleAsync(user, "User");
            await signInManager.SignInAsync(user, false);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (!isUserLogged)
            {
                return Unauthorized();
            }

            var user = await userService.LoggedUserAsync(userId);

            return Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> EditUserProfile([FromForm] UserProfileDto userDto)
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (!isUserLogged)
            {
                return Unauthorized();
            }

            var users = await userService.GetAllUsersExceptCurrentOneAsync(userId);
            var user = await userService.GetLoggedUserAsync(userId);

            if (users.Any(u => u.Email == userDto.Email) && user.Email != userDto.Email)
            {
                ModelState.AddModelError("Email", EmailAlreadyExist);
            }

            if (userDto.Username != null && users.Any(u => u.UserName!.ToLower() == userDto.Username.ToLower()) && user.UserName != userDto.Username)
            {
                ModelState.AddModelError("Username", UserNameAlreadyExist);
            }

            if (userDto.ImageFile != null && userDto.ImageFile.Length > 0)
            {
                var filePath = await imageService.SaveImageAsync(userDto.ImageFile);
                userDto.Image = filePath;
            }


            userDto.Image ??= "profile.jpg";

            if (!string.IsNullOrWhiteSpace(userDto.OldPassword))
            {
                var isCorrectPassword = await userManager.CheckPasswordAsync(user, userDto.OldPassword);

                if (!isCorrectPassword)
                {
                    return BadRequest(InvalidPassword);
                }

                if (userDto.NewPassword == null)
                {
                    return BadRequest(EmptyNewPasswordField);
                }
                else if (userDto.NewPassword != null && userDto.RepeatNewPassword == null)
                {
                    return BadRequest(EmptyRepeatNewPasswordField);
                }
                else if (userDto.NewPassword != userDto.RepeatNewPassword)
                {
                    return BadRequest(PasswordDoesntMatch);
                }



                var result = await userManager.ChangePasswordAsync(user, userDto.OldPassword, userDto.NewPassword!);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await userService.EditUserDetailsAsync(userDto, userId);

            return Ok();
        }

        [HttpPut("delete")]
        public async Task<IActionResult> SoftDeleteUser()
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (isUserLogged)
            {
                await userService.SoftDeleteUser(userId);

                return Ok(SuccessfullDeleteUser);
            }

            return Unauthorized();
        }

        [HttpGet("articles")]
        public async Task<IActionResult> GetUserArticles(int page = DefaultPage, int pageSize = MaxPageSizeUserArticles)
        {
            var isUserLogged = Guid.TryParse(User.GetId(), out Guid userId);

            if (isUserLogged)
            {
                var (articles, paginationMetadata) = await articleService.GetUserArticlesAsync(userId, page, pageSize);

                if (!articles.Any())
                {
                    return NotFound(NotFountArticles);
                }

                Response.Headers.Append("Pagination", JsonSerializer.Serialize(paginationMetadata));
                return Ok(articles);
            }

            return Unauthorized();
        }

    }

}
