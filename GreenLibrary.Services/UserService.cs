namespace GreenLibrary.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;

    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;
    using GreenLibrary.Services.Interfaces;

    [Authorize]
    public class UserService : IUserService
    {
        private readonly GreenLibraryDbContext dbContext;

        public UserService(GreenLibraryDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserSettingsDto> LoggedUserAsync(Guid userId)
        {
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId && u.IsDeleted == false)
                .Select(u => new UserSettingsDto()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email!,
                    Username = u.UserName!,
                    Image = u.Image
                })
                .FirstAsync();

            return user;
        }

        public async Task<User> GetLoggedUserAsync(Guid userId)
        {
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            return user;
        }

        public async Task<User?> EditUserDetailsAsync(UserSettingsDto userDto, Guid userId)
        {
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId && u.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.UserName = userDto.Username;
                user.Email = userDto.Email;
                user.Image = !string.IsNullOrEmpty(userDto.Image) ? userDto.Image : "profile.jpg";

                await dbContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task<IEnumerable<User>> GetAllUsersExceptCurrentOneAsync(Guid uesrId)
        {
            var users = await dbContext
                .Users
                .Where(u => u.Id != uesrId && u.IsDeleted == false)
                .ToListAsync();

            return users;
        }

        public async Task<UserProfileDto?> GetUserProfile(Guid userId)
        {
            var user = await dbContext
        .Users
        .Where(u => u.Id == userId && u.IsDeleted == false)
        .Include(u=>u.Followers)
        .Include(u=>u.Articles)
        .Select(u => new UserProfileDto()
        {
            Id = u.Id,
            Username = u.UserName,
            FirstName = u.FirstName,
            LastName = u.LastName,
            Image = u.Image,
            ArticlesCount = u.Articles.Count(),
            FollowersCount = u.Followers.Count(),
            Followers = u.Followers.Select(f => new UserFollowerDto
            {
                Id = f.Id,
                FirstName = f.FirstName,
                LastName = f.LastName,
                Username = f.UserName,
                IsFollowing = true
            }).ToList()

        })
        .FirstOrDefaultAsync();

            return user;
        }

        public async Task FollowerUser(Guid currUserId, Guid followUserId)
        {
            var currUser = await dbContext
                .Users
                .Where(u => u.IsDeleted == false
                && u.Id == currUserId)
                .FirstOrDefaultAsync();

            var followingUser = await dbContext
                .Users
                .Where(u => u.IsDeleted == false
                && u.Id == followUserId)
                .FirstOrDefaultAsync();

            if ((currUser != null && followingUser != null) && (currUserId != followUserId))
            {
                ((List<User>)currUser.Following).Add(followingUser);
                await dbContext.SaveChangesAsync();
            }

        }

        public async Task UnFollowerUser(Guid currUserId, Guid followUserId)
        {
            var currUser = await dbContext
                .Users
                .Include(u=>u.Following)
                .Where(u => u.IsDeleted == false
                && u.Id == currUserId)
                .FirstOrDefaultAsync();

            var followingUser = await dbContext
                .Users
                .Where(u => u.IsDeleted == false
                && u.Id == followUserId)
                .FirstOrDefaultAsync();

            if (currUser != null && followingUser != null)
            {
                if(currUser.Following.Any(u=>u.Id == followUserId)){
                    ((List<User>)currUser.Following).Remove(followingUser);
                    await dbContext.SaveChangesAsync();
                }
            }

        }

        public async Task<IEnumerable<UserFollowerDto>> GetUserFollowingAsync(Guid userId)
        {
            var currUser = await dbContext
                .Users
                .Where(u => u.Id == userId && u.IsDeleted == false)
                .Include(u => u.Following)
                .FirstAsync();

            var following = currUser.Following
                .Select(u => new UserFollowerDto()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.UserName,
                    IsFollowing = true
                })
                .ToList();

            return following;

        }

        public async Task SoftDeleteUser(Guid userId)
        {
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            user.IsDeleted = true;

            await dbContext.SaveChangesAsync();
        }

    }
}
