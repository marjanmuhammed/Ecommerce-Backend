using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task<List<WishlistItemDto>> GetWishlistByUserIdAsync(int userId)
        {
            var items = await _wishlistRepository.GetWishlistByUserIdAsync(userId);

            return items.Select(w => new WishlistItemDto
            {
                Id = w.Id,
                ProductId = w.ProductId,
                ProductName = w.Product.Name,
                ProductImageUrl = w.Product.ImageUrl,
                ProductPrice = w.Product.Price,
                ProductDescription = w.Product.Description,

            }).ToList();
        }

        public async Task<bool> AddToWishlistAsync(int userId, int productId)
        {
            // Check if already in wishlist (optional)
            var existingItems = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
            if (existingItems.Any(w => w.ProductId == productId))
                return false;  // Already exists

            var newItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId
            };

            await _wishlistRepository.AddWishlistItemAsync(newItem);
            await _wishlistRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(int userId, int wishlistItemId)
        {
            var item = await _wishlistRepository.GetWishlistItemByIdAsync(wishlistItemId);
            if (item == null || item.UserId != userId)
                return false;

            await _wishlistRepository.RemoveWishlistItemAsync(item);
            await _wishlistRepository.SaveChangesAsync();
            return true;
        }
    }
}
