using System.Threading.Tasks;
using Ecommerce_Backend.Interfaces.Admin;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.Data;
using Ecommerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Backend.DTOs.admin;

namespace Ecommerce_Backend.Repositories.Admin
{
    public class AdminCategoryRepository : IAdminCategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminCategoryRepository(ApplicationDbContext db) => _db = db;

        public async Task<int> AddCategoryAsync(AdminCreateCategoryDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
            return category.Id;
        }

        public async Task<bool> UpdateCategoryAsync(int categoryId, AdminUpdateCategoryDTO dto)
        {
            var category = await _db.Categories.FindAsync(categoryId);
            if (category == null) return false;

            if (!string.IsNullOrEmpty(dto.Name))
            {
                category.Name = dto.Name;
            }

            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var category = await _db.Categories.FindAsync(categoryId);
            if (category == null) return false;

            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
            return true;
        }
        public async Task<AdminCategoryDetailsDTO?> GetCategoryByIdAsync(int categoryId)
        {
            return await _db.Categories
                .Where(c => c.Id == categoryId)
                .Select(c => new AdminCategoryDetailsDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AdminCategoryDetailsDTO>> GetAllCategoriesAsync()
        {
            return await _db.Categories
                .Select(c => new AdminCategoryDetailsDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
