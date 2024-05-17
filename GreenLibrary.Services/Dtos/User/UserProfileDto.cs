namespace GreenLibrary.Services.Dtos.User
{

    public class UserProfileDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Image { get; set; } = null!;

        public int ArticlesCount { get; set; }

        public int FollowersCount { get; set; }
    }
}
