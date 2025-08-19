using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.DTOs.admin;
using Ecommerce_Backend.Interfaces.Admin;

namespace Ecommerce_Backend.Services.Admin
{
    public class AdminCategoryService : IAdminCategoryService
    {
        private readonly IAdminCategoryRepository _repo;

        public AdminCategoryService(IAdminCategoryRepository repo)
        {
            _repo = repo;
        }

        public Task<int> AddCategoryAsync(AdminCreateCategoryDTO dto) => _repo.AddCategoryAsync(dto);

        public Task<bool> UpdateCategoryAsync(int categoryId, AdminUpdateCategoryDTO dto) =>
            _repo.UpdateCategoryAsync(categoryId, dto);

        public Task<bool> DeleteCategoryAsync(int categoryId) => _repo.DeleteCategoryAsync(categoryId);

        public Task<AdminCategoryDetailsDTO?> GetCategoryByIdAsync(int categoryId) =>
            _repo.GetCategoryByIdAsync(categoryId);

        public Task<IEnumerable<AdminCategoryDetailsDTO>> GetAllCategoriesAsync() =>
            _repo.GetAllCategoriesAsync();
    }
}
