namespace GreenLibrary.Services.Tests
{
    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using static GreenLibrary.Common.ApplicationConstants;

    public static class DatabaseSeeder
    {
        private static User? User;
        private static Article? Article;

        public static void SeedDatabase(GreenLibraryDbContext dbContext)
        {
            User = new User()
            {
                Id = Guid.Parse("850e61e1-42d2-4632-aa00-0ccdf6c9784c"),
                UserName = "testuser",
                NormalizedUserName = "testuser".ToUpper(),
                Email = "testuser@test.com",
                NormalizedEmail = "testuser@test.com".ToUpper(),
                PasswordHash = "AQAAAAEAACcQAAAAELe8pVu/pYozNSb46Onf++v8jGIFZKhEXaIX0ajNLPc72g7IlIwIgqq5ERU1v4LoYQ==",
                ConcurrencyStamp = "1f21023a-57d9-45b1-8b25-add0035ce3c1",
                SecurityStamp = "SGOWNX4SXZ3DIQOGFWKIYXUJF6IYLSV3",
                FirstName = "Kremena",
                LastName = "Nikolova",
                Image = DefaultImageName
            };

            dbContext.Users.Add(User);

            Article = new Article()
            {
                Id = Guid.Parse("57c07e68-da13-4690-977d-9953c2c8ca7b"),
                Title = "Title 1",
                CreatedOn = DateTime.UtcNow,
                CategoryId = 2,
                Description = "mi in nulla posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus semper eget duis at tellus at urna condimentum mattis pellentesque id nibh tortor id aliquet lectus proin nibh nisl condimentum id venenatis a condimentum vitae sapien pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas sed tempus urna et pharetra pharetra massa massa ultricies mi quis hendrerit dolor magna eget est lorem ipsum dolor sit amet consectetur adipiscing elit pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas integer eget aliquet nibh praesent tristique magna sit amet purus gravida quis blandit turpis cursus in hac habitasse platea dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc congue nisi vitae suscipit tellus mauris a diam maecenas sed enim ut sem viverra aliquet eget sit amet tellus cras adipiscing enim eu turpis egestas pretium aenean pharetra magna ac placerat vestibulum lectus mauris ultrices eros in cursus turpis massa tincidunt dui ut ornare lectus sit amet est placerat in egestas erat imperdiet sed euismod nisi porta lorem mollis aliquam ut porttitor leo a diam sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae aliquet nec ullamcorper sit amet risus nullam eget",
                Image = DefaultImageName,
                UserId = User.Id,
                IsApproved = true,
            };
            dbContext.Articles.Add(Article);

            Article = new Article()
            {
                Id = Guid.NewGuid(),
                Title = "Title 2",
                CreatedOn = DateTime.UtcNow,
                CategoryId = 1,
                Description = "mi in nulla posuere sollicitudin aliquam ultrices sagittis orci a scelerisque purus semper eget duis at tellus at urna condimentum mattis pellentesque id nibh tortor id aliquet lectus proin nibh nisl condimentum id venenatis a condimentum vitae sapien pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas sed tempus urna et pharetra pharetra massa massa ultricies mi quis hendrerit dolor magna eget est lorem ipsum dolor sit amet consectetur adipiscing elit pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas integer eget aliquet nibh praesent tristique magna sit amet purus gravida quis blandit turpis cursus in hac habitasse platea dictumst quisque sagittis purus sit amet volutpat consequat mauris nunc congue nisi vitae suscipit tellus mauris a diam maecenas sed enim ut sem viverra aliquet eget sit amet tellus cras adipiscing enim eu turpis egestas pretium aenean pharetra magna ac placerat vestibulum lectus mauris ultrices eros in cursus turpis massa tincidunt dui ut ornare lectus sit amet est placerat in egestas erat imperdiet sed euismod nisi porta lorem mollis aliquam ut porttitor leo a diam sollicitudin tempor id eu nisl nunc mi ipsum faucibus vitae aliquet nec ullamcorper sit amet risus nullam eget",
                Image = DefaultImageName,
                UserId = User.Id,
                IsApproved = true,
            };
            dbContext.Articles.Add(Article);

            dbContext.SaveChanges();
        }
    }
}
