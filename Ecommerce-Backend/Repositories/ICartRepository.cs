using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId);
        Task<CartItem> GetByIdAsync(int id);
        Task<CartItem> GetByUserAndProductAsync(int userId, int productId);
        Task AddAsync(CartItem item);
        void Remove(CartItem item);
        Task SaveChangesAsync();
    }
}
