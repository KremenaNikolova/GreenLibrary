namespace GreenLibrary.Data.Configurations
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using GreenLibrary.Data.Entities;
    using static GreenLibrary.Common.ApplicationConstants;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasData(GenerateUsers());
        }

        private static User[] GenerateUsers()
        {
            var users = new List<User>();

            User user;
            PasswordHasher<User> ph;

            user = new User()
            {
                Id = new Guid(AdminId),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true,
                FirstName = AdminFirstName,
                LastName = AdminLastName,
                UserName = AdminUsername,
                NormalizedUserName = AdminUsername.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                Image = "profile.jpg",
                IsModerator = true
            };
            ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, AdminPassword);
            users.Add(user);

            user = new User()
            {
                Id = new Guid(ModeratorId),
                Email = ModeratorEmail,
                NormalizedEmail = ModeratorEmail.ToUpper(),
                EmailConfirmed = true,
                FirstName = ModeratorFirstName,
                LastName = ModeratorLastName,
                UserName = ModeratorUsername,
                NormalizedUserName = ModeratorUsername.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                Image = "profile.jpg",
                IsModerator = true
            };
            ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, ModeratorPassword);
            users.Add(user);

            user = new User()
            {
                Id = new Guid(UserId),
                Email = UserEmail,
                NormalizedEmail = UserEmail.ToUpper(),
                EmailConfirmed = true,
                FirstName = UserFirstName,
                LastName = UserLastName,
                UserName = UserUsername,
                NormalizedUserName = UserUsername.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                Image = "profile.jpg"
            };
            ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, UserPassword);
            users.Add(user);

            return users.ToArray();
        }
    }
}
