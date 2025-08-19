using Ecommerce_Backend.DTOs.userprofile;

public interface IUserProfileService
{
    Task<UserProfileDto?> GetUserProfileAsync(string email);
    Task<bool> UpdateUserProfileAsync(string email, UpdateUserProfileDto dto);
    Task<bool> ChangePasswordAsync(string email, ChangePasswordDto dto);
}
