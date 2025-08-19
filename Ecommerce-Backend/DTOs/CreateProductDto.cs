using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class CreateProductDto
{
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Name length should be between 3 and 100 characters")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
    public string Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public double Price { get; set; }

    [Required(ErrorMessage = "CategoryId is required")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Image is required")]
    public IFormFile Image { get; set; }
}
