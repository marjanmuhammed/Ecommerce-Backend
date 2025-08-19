namespace Ecommerce_Backend.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string Source { get; set; }

        public int StatusCode { get; set; }

    }
}
