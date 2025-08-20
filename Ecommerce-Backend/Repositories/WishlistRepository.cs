using Ecommerce_Backend.Data;
using Ecommerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationDbContext _context;

        public WishlistRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WishlistItem>> GetWishlistByUserIdAsync(int userId)
        {
            return await _context.WishlistItems
                .Where(w => w.UserId == userId)
                .Include(w => w.Product)
                .ToListAsync();
        }

        public async Task<WishlistItem?> GetWishlistItemByIdAsync(int id)
        {
            return await _context.WishlistItems
                .Include(w => w.Product)
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task AddWishlistItemAsync(WishlistItem item)
        {
            await _context.WishlistItems.AddAsync(item);
        }

        public async Task RemoveWishlistItemAsync(WishlistItem item)
        {
            _context.WishlistItems.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
