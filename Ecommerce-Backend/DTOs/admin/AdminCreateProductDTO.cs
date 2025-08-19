using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Backend.DTOs.Admin
{
    public class AdminCreateProductDTO
    {
        [Required] public string Name { get; set; } = "";
        [Required] public string Description { get; set; } = "";
        [Required] public decimal Price { get; set; }
        [Required] public int CategoryId { get; set; }
        [Required] public IFormFile Image { get; set; }
    }
}
