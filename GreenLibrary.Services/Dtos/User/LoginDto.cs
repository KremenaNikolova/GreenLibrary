namespace GreenLibrary.Services.Dtos.User
{
    using System.ComponentModel.DataAnnotations;

    public class LoginDto
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
