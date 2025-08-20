using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Microsoft.EntityFrameworkCore;
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
                ProductName = w.Product?.Name ?? "Unknown Product",
                ProductImageUrl = w.Product?.ImageUrl ?? "",
                ProductPrice = w.Product?.Price ?? 0,
                ProductDescription = w.Product?.Description ?? "",
                ProductCategory = w.Product?.Category?.Name ?? "Uncategorized"
            }).ToList();
        }



        // Services/WishlistService.cs
        public async Task<WishlistItemDto> AddToWishlistAsync(int userId, int productId)
        {
            var existingItems = await _wishlistRepository.GetWishlistByUserIdAsync(userId);
            if (existingItems.Any(w => w.ProductId == productId))
                return null; // already exists

            var newItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId
            };

            await _wishlistRepository.AddWishlistItemAsync(newItem);
            await _wishlistRepository.SaveChangesAsync();

            // Reload the wishlist item with Product included
            var savedItem = await _wishlistRepository.GetWishlistItemByIdAsync(newItem.Id);

            return new WishlistItemDto
            {
                Id = savedItem.Id,
                ProductId = savedItem.ProductId,
                ProductName = savedItem.Product?.Name,
                ProductImageUrl = savedItem.Product?.ImageUrl,
                ProductPrice = savedItem.Product?.Price ?? 0,
                ProductDescription = savedItem.Product?.Description,
                ProductCategory = savedItem.Product?.Category?.Name ?? "Uncategorized"
            };
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
