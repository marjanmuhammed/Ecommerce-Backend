using Ecommerce_Backend.Data;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.Interfaces.Admin;
using Ecommerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories.Admin
{
    public class AdminProductRepository : IAdminProductRepository
    {
        private readonly ApplicationDbContext _db;
        public AdminProductRepository(ApplicationDbContext db) => _db = db;

        public async Task<int> AddProductAsync(AdminCreateProductDTO dto, string imageUrl)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                ImageUrl = imageUrl
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product.Id;
        }

        public async Task<bool> UpdateProductAsync(int productId, AdminUpdateProductDTO dto, string? imageUrl = null)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return false;

            if (!string.IsNullOrEmpty(dto.Name)) product.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description)) product.Description = dto.Description;
            if (dto.Price.HasValue) product.Price = dto.Price.Value;
            if (dto.CategoryId.HasValue) product.CategoryId = dto.CategoryId.Value;
            if (!string.IsNullOrEmpty(imageUrl)) product.ImageUrl = imageUrl;

            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return false;

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<AdminAllProductsCategoryDTO>> GetAllProductsAsync()
        {
            return await _db.Products
                .Include(p => p.Category)
                .Select(p => new AdminAllProductsCategoryDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AdminAllProductsCategoryDTO>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _db.Products
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .Select(p => new AdminAllProductsCategoryDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();
        }
    }
}
