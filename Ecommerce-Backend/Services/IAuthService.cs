using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterAsync(RegisterDto dto);
        Task<ApiResponse<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
    }
}
