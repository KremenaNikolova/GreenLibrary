namespace GreenLibrary.Services.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using static GreenLibrary.Common.ErrorMessages.UserErrorMessages;
    using static GreenLibrary.Common.ValidationConstants.UserConstants;

    public class UserProfileDto
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
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = RequiredField)]
        [RegularExpression(EmailPattern, ErrorMessage = InvalidEmail)]
        [StringLength(EmailMaxLength)]
        [Display(Name = "Имейл")]
        public string Email { get; set; } = null!;

        public string? Image { get; set; }

        public IFormFile? ImageFile { get; set; }

        [StringLength(PasswordMaxLength, ErrorMessage = InvalidPasswordLength)]
        [Display(Name = "Парола")]
        public string? OldPassword { get; set; }

        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = InvalidPasswordLength)]
        [RegularExpression(PasswordPattern, ErrorMessage = InvalidPasswordPattern)]
        [Display(Name = "Парола")]
        public string? NewPassword { get; set; }

        [StringLength(PasswordMaxLength, MinimumLength = PasswordMinLength, ErrorMessage = InvalidPasswordLength)]
        [RegularExpression(PasswordPattern, ErrorMessage = InvalidPasswordPattern)]
        [Display(Name = "Парола")]
        public string? RepeatNewPassword { get; set; }

    }
}
