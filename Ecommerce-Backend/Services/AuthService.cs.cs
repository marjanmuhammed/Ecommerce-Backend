using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Ecommerce_Backend.Helpers;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepo.GetByEmailAddressAsync(dto.EmailAddress);
            if (existingUser != null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Email already registered",
                    StatusCode = 409
                };
            }

            var user = new User
            {
                FullName = dto.FullName,
                EmailAddress = dto.EmailAddress,
                Role = "user",
                RefreshToken = JwtHelper.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

            await _userRepo.AddUserAsync(user);
            await _userRepo.SaveChangesAsync();

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Registration successful",
                StatusCode = 201
            };
        }

        public async Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto)
        {
            var user = await _userRepo.GetByEmailAddressAsync(dto.EmailAddress);
            if (user == null)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Invalid email",
                    StatusCode = 404
                };
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                return new ApiResponse<LoginResponseDto>
                {
                    Success = false,
                    Message = "Invalid password",
                    StatusCode = 401
                };
            }

            string token = JwtHelper.GenerateAccessToken(user, _config);
            string refreshToken = JwtHelper.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userRepo.SaveChangesAsync();

            return new ApiResponse<LoginResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = new LoginResponseDto
                {
                    Token = token,
                    RefreshToken = refreshToken,
                    Role = user.Role,
                    IsBlocked = user.IsBlocked
                },
                StatusCode = 200
            };
        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepo.GetByRefreshTokenAsync(refreshToken);
            if (user == null || user.RefreshTokenExpiryTime < DateTime.Now)
            {
                return new AuthResult { Success = false, Message = "Invalid or expired refresh token" };
            }

            string newAccessToken = JwtHelper.GenerateAccessToken(user, _config);
            string newRefreshToken = JwtHelper.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            await _userRepo.SaveChangesAsync();

            return new AuthResult
            {
                Success = true,
                Data = new AuthResultData      
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                }
            };
        }
    }
}
