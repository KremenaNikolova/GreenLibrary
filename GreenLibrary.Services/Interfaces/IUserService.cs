namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;

    public interface IUserService
    {
        Task<UserProfileDto> LoggedUserAsync(Guid userId);

        Task<User?> EditUserDetailsAsync(UserProfileDto userDto, Guid userId);

        Task<User> GetLoggedUserAsync(Guid userId);

        Task<IEnumerable<User>> GetAllUsersExceptCurrentOneAsync(Guid uesrId);

        Task SoftDeleteUser(Guid userId);
    }
}
