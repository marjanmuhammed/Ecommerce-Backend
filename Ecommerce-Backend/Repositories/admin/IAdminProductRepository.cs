using Ecommerce_Backend.DTOs.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Interfaces.Admin
{
    public interface IAdminProductRepository
    {
        Task<int> AddProductAsync(AdminCreateProductDTO dto, string imageUrl);
        Task<bool> UpdateProductAsync(int productId, AdminUpdateProductDTO dto, string? imageUrl = null);
        Task<bool> DeleteProductAsync(int productId);
        Task<IEnumerable<AdminAllProductsCategoryDTO>> GetAllProductsAsync();
        Task<IEnumerable<AdminAllProductsCategoryDTO>> GetProductsByCategoryAsync(int categoryId);
    }
}
