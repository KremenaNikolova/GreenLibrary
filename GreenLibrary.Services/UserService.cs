namespace GreenLibrary.Services
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.EntityFrameworkCore;

    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Services.Helpers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    [Authorize]
    public class UserService : IUserService
    {
        private readonly GreenLibraryDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;

        public UserService(GreenLibraryDbContext dbContext, UserManager<User> userManager, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.configuration = configuration;

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
                .Include(u => u.Followers)
                .Include(u => u.Articles)
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
                .Include(u => u.Following)
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
                if (currUser.Following.Any(u => u.Id == followUserId))
                {
                    ((List<User>)currUser.Following).Remove(followingUser);
                    await dbContext.SaveChangesAsync();
                }
            }

        }

        public async Task<(IEnumerable<UserFollowerDto>, PaginationMetadata)> GetUserFollowingAsync(Guid userId, int currentPage, int pageSize)
        {
            var followingsQuery = dbContext.Users
                .Where(u => u.Followers.Any(f => f.Id == userId))
                .Select(u => new UserFollowerDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.UserName,
                    IsFollowing = true
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(followingsQuery, currentPage, pageSize);
            return result;

        }

        public async Task<(IEnumerable<UserFollowerDto>, PaginationMetadata)> GetUserFollersAsync(Guid userId, int currentPage, int pageSize)
        {
            var followersQuery = dbContext.Users
                .Where(u => u.Following.Any(f => f.Id == userId))
                .Select(u => new UserFollowerDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.UserName,
                    IsFollowing = true
                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(followersQuery, currentPage, pageSize);
            return result;

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

        public async Task RestoreUser(Guid userId)
        {
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
                .FirstAsync();

            user.IsDeleted = false;

            await dbContext.SaveChangesAsync();
        }

        public async Task<(IEnumerable<UserDto>, PaginationMetadata)> GetAllUsersAsync (int currentPage, int pageSize)
        {
            var users = dbContext
                .Users
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsDeleted = u.IsDeleted,
                    IsModerator = u.IsModerator

                })
                .AsQueryable();

            var result = await PaginationHelper.CreatePaginatedResponseAsync(users, currentPage, pageSize);
            return result;
        }

        public async Task ToggleModerator(Guid userId)
        {
            var user = await dbContext
                .Users
                .Where (u => u.Id == userId)
            .FirstAsync();

            user.IsModerator = !user.IsModerator;

            await dbContext.SaveChangesAsync();
        }

        public async Task<(string, string)> TokenAndClaimsConfig(User user)
        {
            var roles = await userManager.GetRolesAsync(user!);

            var claims = new List<Claim>
                    {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, roles[0]),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                    };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));
            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: configuration.GetSection("Jwt:Issuer").Value,
                audience: configuration.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return (tokenString, roles[0]);
        }

    }
}
