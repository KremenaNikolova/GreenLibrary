﻿namespace GreenLibrary.Services
{
    using System.Threading.Tasks;

    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    using GreenLibrary.Data;
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;
    using GreenLibrary.Services.Interfaces;
    using GreenLibrary.Services.Helpers;
    using static GreenLibrary.Common.ApplicationConstants;

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

        public async Task<(IEnumerable<UserDto>, PaginationMetadata)> GetAllUsersAsync(int currentPage, int pageSize, string sortBy)
        {
            var users = dbContext
                .Users
                .AsQueryable();

            var sortedUsers = SortUsers(users, sortBy);

            var result = await PaginationHelper.CreatePaginatedResponseAsync(sortedUsers, currentPage, pageSize);
            return result;
        }

        public async Task ToggleModerator(Guid userId)
        {
            var user = await dbContext
                .Users
                .Where(u => u.Id == userId)
            .FirstAsync();

            user.IsModerator = !user.IsModerator;

            if (user.IsModerator == true)
            {
                await userManager.RemoveFromRoleAsync(user, UserRoleName);
                await userManager.AddToRoleAsync(user, ModeratorRoleName);
            }
            else
            {
                await userManager.RemoveFromRoleAsync(user, ModeratorRoleName);
                await userManager.AddToRoleAsync(user, UserRoleName);
            }

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

        private IQueryable<UserDto> SortUsers(IQueryable<User> userDto, string sortBy)
        {
            switch (sortBy.ToLower())
            {
                case "username-asc":
                    userDto = userDto
                        .OrderBy(p => p.UserName)
                        .ThenBy(p=>p.FirstName)
                        .ThenBy(p => p.LastName);
                    break;
                case "firstname-asc":
                    userDto = userDto
                        .OrderBy(p => p.FirstName)
                        .ThenBy(p => p.LastName)
                        .ThenBy(p => p.UserName);
                    break;
                case "lastname-asc":
                    userDto = userDto.OrderBy(p => p.LastName)
                        .ThenBy(p => p.FirstName)
                        .ThenBy(p => p.UserName);
                    break;
                case "createon-newest":
                    userDto = userDto.OrderByDescending(p => p.CreatedOn)
                        .ThenBy(p => p.FirstName)
                        .ThenBy(p => p.LastName);
                    break;
                case "createon-oldest":
                    userDto = userDto.OrderBy(p => p.CreatedOn)
                        .ThenBy(p => p.FirstName)
                        .ThenBy(p => p.LastName);
                    break;
                case "moderators":
                    userDto = userDto
                        .OrderByDescending(p => p.IsModerator == true)
                        .ThenBy(p=>p.FirstName)
                        .ThenBy(p=>p.LastName);
                    break;
                default:
                    userDto = userDto.OrderByDescending(p => p.CreatedOn);
                    break;
            }

            var result = userDto
                .Select(u => new UserDto()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    CreatedOn = u.CreatedOn.ToString("d"),
                    IsDeleted = u.IsDeleted,
                    IsModerator = u.IsModerator
                })
                .AsQueryable();

            return result;
        }

    }
}
