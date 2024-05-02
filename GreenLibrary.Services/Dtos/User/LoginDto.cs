namespace GreenLibrary.Services.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    using static GreenLibrary.Common.ErrorMessages.UserErrorMessages;

    public class LoginDto
    {
        [Required(ErrorMessage = RequiredField)]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;
    }
}
