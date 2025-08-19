using Ecommerce_Backend.Data;
using Ecommerce_Backend.DTOs.Cart;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repo;
        private readonly ApplicationDbContext _db;

        public CartService(ICartRepository repo, ApplicationDbContext db)
        {
            _repo = repo;
            _db = db;
        }

        public async Task<(bool Success, string Error, CartItemDTO Item)> AddToCartAsync(int userId, AddCartItemDTO dto)
        {
            if (dto.Quantity <= 0)
                return (false, "Quantity must be greater than zero", null);

            // Check if product exists
            var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == dto.ProductId);
            if (product == null)
                return (false, "Product not found", null);

            // Check if product already in user's cart
            var existing = await _repo.GetByUserAndProductAsync(userId, dto.ProductId);
            if (existing != null)
            {
                // Product already in cart — return existing item (don't increase quantity automatically)
                return (false, "Product already in cart", MapToDto(existing));
            }

            // Create new cart item
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                AddedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(cartItem);
            await _repo.SaveChangesAsync();

            // Reload with product navigation property for DTO mapping
            var saved = await _repo.GetByIdAsync(cartItem.Id);
            return (true, null, MapToDto(saved));
        }
        public async Task<IEnumerable<CartItemDTO>> GetUserCartAsync(int userId)
        {
            var items = await _db.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product) // Load product automatically
                .ToListAsync();

            var result = items.Select(c => new CartItemDTO
            {
                Id = c.Id,
                Quantity = c.Quantity,
                ProductId = c.ProductId,
                ProductName = c.Product?.Name ?? "",
                ProductDescription = c.Product?.Description ?? "",
                ProductPrice = c.Product?.Price ?? 0,
                ProductImageUrl = c.Product?.ImageUrl ?? ""
            });

            return result;
        }


        public async Task<(bool Success, string Error)> RemoveAsync(int userId, int cartItemId)
        {
            var item = await _repo.GetByIdAsync(cartItemId);
            if (item == null || item.UserId != userId)
                return (false, "Cart item not found or unauthorized");

            _repo.Remove(item);
            await _repo.SaveChangesAsync();
            return (true, null);
        }

        public async Task<(bool Success, string Error)> UpdateQuantityAsync(int userId, int cartItemId, int quantity)
        {
            if (quantity <= 0)
                return (false, "Quantity must be greater than zero");

            var item = await _repo.GetByIdAsync(cartItemId);
            if (item == null || item.UserId != userId)
                return (false, "Cart item not found or unauthorized");

            item.Quantity = quantity;
            await _repo.SaveChangesAsync();
            return (true, null);
        }

        private CartItemDTO MapToDto(CartItem c) => new CartItemDTO
        {
            Id = c.Id,
            Quantity = c.Quantity,
            ProductId = c.ProductId,
            ProductName = c.Product?.Name ?? string.Empty,
            ProductPrice = c.Product?.Price ?? 0,
            ProductImageUrl = c.Product?.ImageUrl ?? string.Empty,
            ProductDescription = c.Product?.Description ?? string.Empty  // Add in DTO if needed
        };
    }
}
