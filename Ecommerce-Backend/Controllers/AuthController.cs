using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (!result.Success)
                return StatusCode(result.StatusCode, new { message = result.Message });

            return StatusCode(result.StatusCode, new { message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (!result.Success)
                return StatusCode(result.StatusCode, new { message = result.Message });

            return Ok(new
            {
                token = result.Data.Token,
                refreshToken = result.Data.RefreshToken,
                role = result.Data.Role,
                isBlocked = result.Data.IsBlocked
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            var result = await _authService.RefreshTokenAsync(request.Token);
            if (!result.Success)
                return Unauthorized(new { message = result.Message });

            return Ok(new { token = result.Data.Token, refreshToken = result.Data.RefreshToken });
        }
    }
}
