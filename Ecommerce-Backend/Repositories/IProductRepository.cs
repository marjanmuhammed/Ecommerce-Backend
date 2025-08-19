using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        void Update(Product product);
        void Remove(Product product);
        Task<bool> SaveChangesAsync();

        Task<Category?> GetCategoryByIdAsync(int id);
    }
}
