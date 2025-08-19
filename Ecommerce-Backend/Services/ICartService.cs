using Ecommerce_Backend.DTOs.Cart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDTO>> GetUserCartAsync(int userId);
        Task<(bool Success, string Error, CartItemDTO Item)> AddToCartAsync(int userId, AddCartItemDTO dto);
        Task<(bool Success, string Error)> UpdateQuantityAsync(int userId, int cartItemId, int quantity);
        Task<(bool Success, string Error)> RemoveAsync(int userId, int cartItemId);
    }
}
