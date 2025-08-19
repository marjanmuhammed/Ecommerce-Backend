using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.Helpers;
using Ecommerce_Backend.Interfaces.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminProductsController : ControllerBase
    {
        private readonly CloudinaryHelper _cloudinary;
        private readonly IAdminProductService _adminProductService;

        public AdminProductsController(CloudinaryHelper cloudinary, IAdminProductService adminProductService)
        {
            _cloudinary = cloudinary;
            _adminProductService = adminProductService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _adminProductService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            var products = await _adminProductService.GetProductsByCategoryAsync(categoryId);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] AdminCreateProductDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string imageUrl = await _cloudinary.UploadImageAsync(dto.Image);
            if (string.IsNullOrEmpty(imageUrl))
                return BadRequest("Image upload failed");

            var productId = await _adminProductService.AddProductAsync(dto, imageUrl);
            return Ok(new { id = productId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] AdminUpdateProductDTO dto)
        {
            string? imageUrl = null;
            if (dto.Image != null)
            {
                imageUrl = await _cloudinary.UploadImageAsync(dto.Image);
                if (string.IsNullOrEmpty(imageUrl))
                    return BadRequest("Image upload failed");
            }

            var updated = await _adminProductService.UpdateProductAsync(id, dto, imageUrl);
            if (!updated) return NotFound();

            return Ok(new { message = "Product updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _adminProductService.DeleteProductAsync(id);
            if (!deleted) return NotFound();

            return Ok(new { message = "Product deleted" });
        }
    }
}
