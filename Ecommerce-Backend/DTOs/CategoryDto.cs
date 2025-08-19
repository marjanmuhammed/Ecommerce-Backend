using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Ecommerce_Backend.DTOs
{
    public class CreateProductDto
    {
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        // either provide CategoryId or CategoryName (we'll use CategoryId)
        [Required]
        public int CategoryId { get; set; }

        // for image upload (multipart/form-data)
        public IFormFile Image { get; set; }
    }
}
