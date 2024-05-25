namespace GreenLibrary.Services.Dtos.User
{

    public class UserDto
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public bool IsModerator { get; set; }
    }
}
