using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Backend.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyEcomApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _db.Products
                            .Include(p => p.Category)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _db.Products.AddAsync(product);
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }

        public void Remove(Product product)
        {
            _db.Products.Remove(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _db.SaveChangesAsync()) > 0;
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }
    }
}
