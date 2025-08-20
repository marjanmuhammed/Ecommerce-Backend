using Ecommerce_Backend.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public interface IWishlistService
    {
        Task<List<WishlistItemDto>> GetWishlistByUserIdAsync(int userId);
        Task<WishlistItemDto> AddToWishlistAsync(int userId, int productId);

        Task<bool> RemoveFromWishlistAsync(int userId, int wishlistItemId);
    }
}
