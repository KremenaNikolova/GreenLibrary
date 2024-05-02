namespace GreenLibrary.Server.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;

    using static GreenLibrary.Common.ErrorMessages.UserErrorMessages;

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UserController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(loginDto.Username);
                var roles = await userManager.GetRolesAsync(user);
                HttpContext.Session.SetString("username", loginDto.Username);

                return Ok(new
                {
                    Username = user.UserName,
                    Roles = roles[0]
                });
            }
            return Unauthorized(InvalidUsernameOrPassword);
        }

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
    }

}
