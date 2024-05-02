namespace GreenLibrary.Services.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    using static GreenLibrary.Common.ErrorMessages.UserErrorMessages;
    using static GreenLibrary.Common.ValidationConstants.UserConstants;

    public class RegisterDto
    {
        [Required(ErrorMessage = RequiredField)]
        [StringLength(FirstNameMaxLength, MinimumLength = FirstNameMinLength, ErrorMessage = InvalidFirstName)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = null!;

        [StringLength(LastNameMaxLength, MinimumLength = LastNameMinLength, ErrorMessage = InvalidLastName)]
        [Required(ErrorMessage = RequiredField)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength, ErrorMessage = InvalidUsername)]
        [Display(Name = "Потребителско име")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [RegularExpression(EmailPattern, ErrorMessage = InvalidEmail)]
        [StringLength(EmailMaxLength)]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = InvalidPassword)]
        [Display(Name = "Парола")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = InvalidPassword)]
        [Display(Name = "Повторете паролата")]
        public string RepeatPassword { get; set; } = null!;
    }
}
