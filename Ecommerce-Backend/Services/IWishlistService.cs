using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public interface IWishlistService
    {
        Task<List<WishlistItemDto>> GetWishlistByUserIdAsync(int userId);
        Task<bool> AddToWishlistAsync(int userId, int productId);
        Task<bool> RemoveFromWishlistAsync(int userId, int wishlistItemId);
    }
}
