namespace GreenLibrary.Server.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;

    using static GreenLibrary.Common.ErrorMessages;

    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<User> signInManager;

        public UserController(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
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
                HttpContext.Session.SetString("email", loginDto.Username);
                return Ok();
            }
            return Unauthorized(UserErrorMessages.InvalidUsernameOrPassword);
        }
    }

}
