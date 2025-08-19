using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.Interfaces.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services.Admin
{
    public class AdminProductService : IAdminProductService
    {
        private readonly IAdminProductRepository _repo;
        public AdminProductService(IAdminProductRepository repo) => _repo = repo;

        public Task<int> AddProductAsync(AdminCreateProductDTO dto, string imageUrl)
            => _repo.AddProductAsync(dto, imageUrl);

        public Task<bool> UpdateProductAsync(int id, AdminUpdateProductDTO dto, string? imageUrl = null)
            => _repo.UpdateProductAsync(id, dto, imageUrl);

        public Task<bool> DeleteProductAsync(int id)
            => _repo.DeleteProductAsync(id);

        public Task<IEnumerable<AdminAllProductsCategoryDTO>> GetAllProductsAsync()
            => _repo.GetAllProductsAsync();

        public Task<IEnumerable<AdminAllProductsCategoryDTO>> GetProductsByCategoryAsync(int categoryId)
            => _repo.GetProductsByCategoryAsync(categoryId);
    }
}
