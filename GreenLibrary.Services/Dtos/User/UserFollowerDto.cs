namespace GreenLibrary.Services.Dtos.User
{

    public class UserFollowerDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public bool IsFollowing { get; set; }
    }
}
