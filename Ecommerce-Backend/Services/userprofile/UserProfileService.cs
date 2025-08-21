using Ecommerce_Backend.DTOs.userprofile;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Backend.Services // ✅ Fixed namespace
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserProfileService(IUserRepository userRepo, IPasswordHasher<User> passwordHasher)
        {
            _userRepo = userRepo;
            _passwordHasher = passwordHasher;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(string email)
        {
            var user = await _userRepo.GetByEmailAddressAsync(email);
            if (user == null) return null;

            return new UserProfileDto
            {
                FullName = user.FullName,
                Email = user.EmailAddress
            };
        }

        public async Task<bool> UpdateUserProfileAsync(string email, UpdateUserProfileDto dto)
        {
            var user = await _userRepo.GetByEmailAddressAsync(email);
            if (user == null) return false;

            user.FullName = dto.FullName;
            user.EmailAddress = dto.Email;

            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(string email, ChangePasswordDto dto)
        {
            var user = await _userRepo.GetByEmailAddressAsync(email);
            if (user == null) return false;

            // Verify current password
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.CurrentPassword);
            if (result == PasswordVerificationResult.Failed)
                return false;

            // Hash and set new password
            user.PasswordHash = _passwordHasher.HashPassword(user, dto.NewPassword);
            await _userRepo.SaveChangesAsync();
            return true;
        }
    }
}