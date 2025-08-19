using Microsoft.AspNetCore.Http;

namespace Ecommerce_Backend.DTOs.Admin
{
    public class AdminUpdateProductDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
