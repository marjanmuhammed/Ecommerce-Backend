namespace Ecommerce_Backend.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public string Role { get; set; }

        public bool IsBlocked { get; set; }
    }
}
