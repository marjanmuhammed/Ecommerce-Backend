using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.DTOs.admin;
using Ecommerce_Backend.Interfaces.Admin;

namespace Ecommerce_Backend.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")] // Only admin can access
    public class AdminCategoryController : ControllerBase
    {
        private readonly IAdminCategoryService _categoryService;

        public AdminCategoryController(IAdminCategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/admin/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminCategoryDetailsDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        // GET: api/admin/category/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminCategoryDetailsDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        // POST: api/admin/category
        [HttpPost]
        public async Task<ActionResult<int>> AddCategory([FromBody] AdminCreateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCategoryId = await _categoryService.AddCategoryAsync(dto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = newCategoryId }, newCategoryId);
        }

        // PUT: api/admin/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] AdminUpdateCategoryDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _categoryService.UpdateCategoryAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/admin/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
