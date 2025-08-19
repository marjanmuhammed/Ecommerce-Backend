using Ecommerce_Backend.Models;
using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task<Product> CreateAsync(Product p);
        Task<bool> UpdateAsync(Product p);
        Task<bool> DeleteAsync(int id);
    }
}
