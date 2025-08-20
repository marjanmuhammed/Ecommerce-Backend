using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories
{
    public interface IWishlistRepository
    {
        Task<List<WishlistItem>> GetWishlistByUserIdAsync(int userId);
        Task<WishlistItem?> GetWishlistItemByIdAsync(int id);
        Task AddWishlistItemAsync(WishlistItem item);
        Task RemoveWishlistItemAsync(WishlistItem item);
        Task SaveChangesAsync();
    }
}
