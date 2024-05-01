namespace GreenLibrary.Services.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    using static GreenLibrary.Common.ErrorMessages.UserErrorMessages;

    public class LoginDto
    {
        [Required(ErrorMessage = RequiredField)]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        public string Password { get; set; } = null!;
    }
}
