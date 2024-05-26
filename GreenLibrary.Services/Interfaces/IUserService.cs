namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;
    using GreenLibrary.Services.Helpers;

    public interface IUserService
    {
        Task<UserSettingsDto> LoggedUserAsync(Guid userId);

        Task<User?> EditUserDetailsAsync(UserSettingsDto userDto, Guid userId);

        Task<User> GetLoggedUserAsync(Guid userId);

        Task<IEnumerable<User>> GetAllUsersExceptCurrentOneAsync(Guid uesrId);

        Task<UserProfileDto?> GetUserProfile(Guid userId);

        Task FollowerUser(Guid currUserId, Guid followUserId);

        Task UnFollowerUser(Guid currUserId, Guid followUserId);

        Task<(IEnumerable<UserFollowerDto>, PaginationMetadata)> GetUserFollowingAsync(Guid userId, int currentPage, int pageSize);

        Task<(IEnumerable<UserFollowerDto>, PaginationMetadata)> GetUserFollersAsync(Guid userId, int currentPage, int pageSize);

        Task SoftDeleteUser(Guid userId);

        Task RestoreUser(Guid userId);

        Task<(IEnumerable<UserDto>, PaginationMetadata)> GetAllUsersAsync(int currentPage, int pageSize);

        Task ToggleModerator(Guid userId);
    }
}
