﻿namespace GreenLibrary.Services.Interfaces
{
    using GreenLibrary.Data.Entities;
    using GreenLibrary.Services.Dtos.User;

    public interface IUserService
    {
        Task<UserSettingsDto> LoggedUserAsync(Guid userId);

        Task<User?> EditUserDetailsAsync(UserSettingsDto userDto, Guid userId);

        Task<User> GetLoggedUserAsync(Guid userId);

        Task<IEnumerable<User>> GetAllUsersExceptCurrentOneAsync(Guid uesrId);

        Task<UserProfileDto?> GetUserProfile(Guid userId);

        Task SoftDeleteUser(Guid userId);
    }
}
