namespace Ecommerce_Backend.Models
{
    public class AuthResultData
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }

    public class AuthResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public AuthResultData Data { get; set; }
    }
}
