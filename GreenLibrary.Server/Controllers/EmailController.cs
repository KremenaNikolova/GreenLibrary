namespace GreenLibrary.Server.Controllers
{
    using System.Text;
    
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.Email;
    using GreenLibrary.Services.Interfaces;
    using static GreenLibrary.Common.ErrorMessages.EmailErrorMessages;
    using static GreenLibrary.Common.SuccessfulMessage.EmailSuccesfulMessage;

    [Route("api/email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly UserManager<User> userManager;

        public EmailController(IEmailService emailService, UserManager<User> userManager)
        {
            this.emailService = emailService;
            this.userManager = userManager;
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null || user.IsDeleted)
            {
                return BadRequest(WrongEmailAddress);
            }

            var newPassword = GenerateRandomPassword();
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);

            if (result.Succeeded)
            {
                var emailDto = new EmailDto
                {
                    To = forgotPasswordDto.Email,
                    Subject = "Нулиране на парола",
                    Body = $"Вашата нова парола е: {newPassword}"
                };
                emailService.SendEmail(emailDto);

                return Ok(SuccessfullSendedNewPassword);
            }

            return BadRequest(BadRequestDefaultMessage);
        }

        private string GenerateRandomPassword(int length = 12)
        {
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digitChars = "1234567890";
            const string nonAlphaChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/`~";

            Random rnd = new Random();

            StringBuilder res = new StringBuilder();
            res.Append(lowerChars[rnd.Next(lowerChars.Length)]);
            res.Append(upperChars[rnd.Next(upperChars.Length)]);
            res.Append(digitChars[rnd.Next(digitChars.Length)]);
            res.Append(nonAlphaChars[rnd.Next(nonAlphaChars.Length)]);

            string allChars = lowerChars + upperChars + digitChars + nonAlphaChars;
            for (int i = 4; i < length; i++)
            {
                res.Append(allChars[rnd.Next(allChars.Length)]);
            }

            return new string(res.ToString().OrderBy(c => rnd.Next()).ToArray());
        }
    }
}
