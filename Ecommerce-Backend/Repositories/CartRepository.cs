using Ecommerce_Backend.Data;
using Ecommerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;
        public CartRepository(ApplicationDbContext db) => _db = db;

        public async Task AddAsync(CartItem item) => await _db.CartItems.AddAsync(item);

        public void Remove(CartItem item) => _db.CartItems.Remove(item);

        public async Task<CartItem> GetByIdAsync(int id) =>
            await _db.CartItems.Include(c => c.Product).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<CartItem> GetByUserAndProductAsync(int userId, int productId) =>
            await _db.CartItems.Include(c => c.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

        public async Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId) =>
            await _db.CartItems.Include(c => c.Product)
                .Where(c => c.UserId == userId)
                .ToListAsync();

        public async Task SaveChangesAsync() => await _db.SaveChangesAsync();
    }
}
