namespace GreenLibrary.Services.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Identity;

    using static DatabaseSeeder;
    using GreenLibrary.Data;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;
    using static GreenLibrary.Common.ApplicationConstants;

    using Moq;

    public class UserServiceTests
    {
        private DbContextOptions<GreenLibraryDbContext> dbOptions;
        private GreenLibraryDbContext dbContext;
        private IUserService userService;
        private Mock<UserManager<User>> mockUserManager;
        private Mock<IConfiguration> mockConfiguration;

        Guid userId;
        Guid user2Id;
        int currentPage;
        int pageSize;
        string sortBy;

        private Mock<UserManager<User>> MockUserManager()
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());

            return mgr;
        }
        [SetUp]
        public void SetUp()
        {
            dbOptions = new DbContextOptionsBuilder<GreenLibraryDbContext>()
                .UseInMemoryDatabase("GreenLibraryInMemory" + Guid.NewGuid().ToString())
                .Options;

            dbContext = new GreenLibraryDbContext(dbOptions);
            dbContext.Database.EnsureCreated();

            mockUserManager = MockUserManager();
            mockConfiguration = new Mock<IConfiguration>();

            userService = new UserService(dbContext, mockUserManager.Object, mockConfiguration.Object);
            SeedDatabase(dbContext);

            userId = Guid.Parse("850e61e1-42d2-4632-aa00-0ccdf6c9784c");
            user2Id = Guid.Parse(UserId);
            currentPage = 1;
            pageSize = 5;
            sortBy = "createon-newest";

        }

        [TearDown]
        public void TearDown()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        [Test]
        public async Task LoggedUserAsync_ShouldReturnUserByGivenUserId()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId
                && u.IsDeleted == false)
                .FirstAsync();

            //act
            var result = await userService.LoggedUserAsync(userId);

            //assert
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.Username, Is.EqualTo(user.UserName));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.Image, Is.EqualTo(user.Image));
        }

        [Test]
        public async Task GetLoggedUserAsync_ShouldReturnUserByGivenUserId()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId
                && u.IsDeleted == false)
                .FirstAsync();

            //act
            var result = await userService.GetLoggedUserAsync(userId);

            //assert
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.UserName, Is.EqualTo(user.UserName));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.Image, Is.EqualTo(user.Image));
            Assert.That(result.IsModerator, Is.EqualTo(user.IsModerator));
            Assert.That(result.IsDeleted, Is.EqualTo(user.IsDeleted));
            Assert.That(result, Is.EqualTo(user));
        }

        [Test]
        public async Task EditUserDetailsAsync_ShouldReturnUserWithChangedData()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId
                && u.IsDeleted == false)
                .FirstAsync();

            var userDto = new UserSettingsDto()
            {
                FirstName = "NameTest1",
                LastName = "NameTest2",
                Username = user.UserName,
                Email = user.Email,
                Image = "NewImageName.jpg",
            };

            //act
            var result = await userService.EditUserDetailsAsync(userDto, userId);

            //assert
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(result.UserName, Is.EqualTo(userDto.Username));
            Assert.That(result.FirstName, Is.EqualTo(userDto.FirstName));
            Assert.That(result.LastName, Is.EqualTo(userDto.LastName));
            Assert.That(result.Email, Is.EqualTo(user.Email));
            Assert.That(result.Image, Is.EqualTo(userDto.Image));
            Assert.That(result.IsModerator, Is.EqualTo(user.IsModerator));
            Assert.That(result.IsDeleted, Is.EqualTo(user.IsDeleted));
        }

        [Test]
        public async Task GetAllUsersExceptCurrentOneAsync_ShouldReturnCollectionOfAllNotDeletedUsersExceptOne()
        {
            //arrange
            var allUsersExceptOne = await dbContext
                .Users
                .Where(u => u.Id != userId
                && u.IsDeleted == false)
                .ToListAsync();

            var allUsers = await dbContext
                .Users
                .Where(u => u.IsDeleted == false)
                .ToListAsync();

            //act
            var result = await userService.GetAllUsersExceptCurrentOneAsync(userId);

            //assert
            Assert.That(result.Count(), Is.EqualTo(allUsersExceptOne.Count()));
            Assert.That(result.Count(), Is.Not.EqualTo(allUsers.Count()));
            Assert.That(result.Count(), Is.EqualTo(allUsers.Count() - 1));
        }

        [Test]
        public async Task GetUserProfile_ShouldGetUserInformationByItsId()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId
                && u.IsDeleted == false)
                .Include(u => u.Followers)
                .Include(u => u.Articles)
                .FirstAsync();

            //act
            var result = await userService.GetUserProfile(userId);

            //assert
            Assert.That(result.Id, Is.EqualTo(userId));
            Assert.That(user.Id, Is.EqualTo(userId));
            Assert.That(result.Username, Is.EqualTo(user.UserName));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
            Assert.That(result.Image, Is.EqualTo(user.Image));
            Assert.That(result.ArticlesCount, Is.EqualTo(user.Articles.Count()));
            Assert.That(result.FollowersCount, Is.EqualTo(user.Followers.Count()));
            Assert.That(result.Followers.Count(), Is.EqualTo(user.Followers.Count()));
            Assert.That(result.FollowersCount, Is.EqualTo(result.Followers.Count()));
        }

        [Test]
        public async Task FollowerUser_ShouldAddUserInFollowingCollectionOfLoggedUser()
        {
            //arrange
            var loggedUser = await dbContext
                .Users
                .Include(u => u.Following)
                .Where(u => u.IsDeleted == false
                && u.Id == userId)
                .FirstOrDefaultAsync();

            var loggedUserFollowingsCountBefore = loggedUser.Following.Count();

            //act
            await userService.FollowerUser(userId, user2Id);

            var loggedUserFollowingsCountAfter = loggedUser.Following.Count();

            //assert
            Assert.That(loggedUserFollowingsCountAfter, Is.Not.EqualTo(loggedUserFollowingsCountBefore));

        }

        [Test]
        public async Task UnFollowerUser_ShouldRemoveUserInFollowingCollectionOfLoggedUser()
        {
            //arrange
            var loggedUser = await dbContext
                .Users
                .Include(u => u.Following)
                .Where(u => u.IsDeleted == false
                && u.Id == userId)
                .FirstOrDefaultAsync();

            await userService.FollowerUser(userId, user2Id);
            var loggedUserFollowingsCountBefore = loggedUser.Following.Count();

            //act
            await userService.UnFollowerUser(userId, user2Id);
            var loggedUserFollowingsCountAfter = loggedUser.Following.Count();

            //assert
            Assert.That(loggedUserFollowingsCountAfter, Is.Not.EqualTo(loggedUserFollowingsCountBefore));

        }

        [Test]
        public async Task GetUserFollowingAsync_ShouldReturnCollectionOfLoggedUserFollowingsViaPaginationMetadata()
        {
            //arrange
            var userFollowings = await dbContext.Users
                .Where(u => u.Followers.Any(f => f.Id == userId))
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await userService.GetUserFollowingAsync(userId, currentPage, pageSize);

            //assert
            Assert.That(userFollowings.Count(), Is.EqualTo(paginationMetadata.TotalItemCount));

        }

        [Test]
        public async Task GetUserFollowingAsync_ShouldReturnCollectionOfLoggedUserFollowings()
        {
            //arrange
            var userFollowings = await dbContext.Users
                .Where(u => u.Followers.Any(f => f.Id == userId))
                .Take(pageSize)
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await userService.GetUserFollowingAsync(userId, currentPage, pageSize);

            //assert
            Assert.That(userFollowings.Count(), Is.EqualTo(result.Count()));

        }

        [Test]
        public async Task GetUserFollowersAsync_ShouldReturnCollectionOfLoggedUserGetUserFollersAsync()
        {
            //arrange
            var userFollowers = await dbContext.Users
                .Where(u => u.Followers.Any(f => f.Id == userId))
                .Take(pageSize)
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await userService.GetUserFollersAsync(userId, currentPage, pageSize);

            //assert
            Assert.That(userFollowers.Count(), Is.EqualTo(result.Count()));

        }

        [Test]
        public async Task GetUserFollowersAsync_ShouldReturnCollectionOfLoggedUserGetUserFollersAsyncViaPaginationMetadata()
        {
            //arrange
            var userFollowers = await dbContext.Users
                .Where(u => u.Followers.Any(f => f.Id == userId))
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await userService.GetUserFollersAsync(userId, currentPage, pageSize);

            //assert
            Assert.That(userFollowers.Count(), Is.EqualTo(result.Count()));

        }

        [Test]
        public async Task SoftDeleteUserc_ShouldSetUserIsDeletePropertyToTrue()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            var isDeletedBefore = user.IsDeleted;

            //act
            await userService.SoftDeleteUser(userId);

            var isDeletedAfter = user.IsDeleted;

            //assert
            Assert.That(isDeletedBefore, Is.Not.EqualTo(isDeletedAfter));

        }

        [Test]
        public async Task RestoreUser_ShouldSetUserIsDeletePropertyToFalse()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            await userService.SoftDeleteUser(userId);
            var isDeletedBefore = user.IsDeleted;

            //act
            await userService.RestoreUser(userId);
            var isDeletedAfter = user.IsDeleted;

            //assert
            Assert.That(isDeletedBefore, Is.Not.EqualTo(isDeletedAfter));

        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnCollectionOfExistingUsers()
        {
            //arrange
            var users = await dbContext
                .Users
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await userService.GetAllUsersAsync(currentPage, pageSize, sortBy);

            //assert
            Assert.That(users.Count(), Is.EqualTo(paginationMetadata.TotalItemCount));
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnCollectionOfExistingUsersInFirstPage()
        {
            //arrange
            var users = await dbContext
                .Users
                .Take(pageSize)
                .ToListAsync();

            //act
            var (result, paginationMetadata) = await userService.GetAllUsersAsync(currentPage, pageSize, sortBy);

            //assert
            Assert.That(users.Count(), Is.EqualTo(result.Count()));
        }

        [Test]
        public async Task GetAllUsersAsync_ShouldReturnCollectionOfExistingUsersInSortByOrder()
        {
            //arrange
            var users = await dbContext
                .Users
                .OrderByDescending(u => u.CreatedOn)
                .Take(pageSize)
                .ToListAsync();

            var user = users.First();

            //act
            var (results, paginationMetadata) = await userService.GetAllUsersAsync(currentPage, pageSize, sortBy);
            var result = results.First();

            //assert
            Assert.That(user.Id, Is.EqualTo(result.Id));
            Assert.That(user.UserName, Is.EqualTo(result.Username));
            Assert.That(user.FirstName, Is.EqualTo(result.FirstName));
            Assert.That(user.LastName, Is.EqualTo(result.LastName));
            Assert.That(user.Email, Is.EqualTo(result.Email));
            Assert.That(user.IsModerator, Is.EqualTo(result.IsModerator));
            Assert.That(user.IsDeleted, Is.EqualTo(result.IsDeleted));
        }

        [Test]
        public async Task ToggleModerator_ShouldChangeStateOfIsModeratorProperty()
        {
            //arrange
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            var isModeratorBefore = user.IsModerator;

            //act
            await userService.ToggleModerator(userId);
            var isModeratorAfter = user.IsModerator;

            //act
            await userService.ToggleModerator(userId);
            var isModeratorAfterAgain = user.IsModerator;

            //assert
            Assert.That(isModeratorBefore, Is.Not.EqualTo(isModeratorAfter));
            Assert.That(isModeratorBefore, Is.EqualTo(isModeratorAfterAgain));
            Assert.That(isModeratorAfterAgain, Is.Not.EqualTo(isModeratorAfter));
        }
    }
}
