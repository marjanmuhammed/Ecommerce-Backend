using Ecommerce_Backend.DTOs.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Interfaces.Admin
{
    public interface IAdminProductService
    {
        Task<int> AddProductAsync(AdminCreateProductDTO dto, string imageUrl);
        Task<bool> UpdateProductAsync(int id, AdminUpdateProductDTO dto, string? imageUrl = null);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<AdminAllProductsCategoryDTO>> GetAllProductsAsync();
        Task<IEnumerable<AdminAllProductsCategoryDTO>> GetProductsByCategoryAsync(int categoryId);
    }
}
