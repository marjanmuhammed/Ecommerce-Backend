using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.DTOs.admin;

namespace Ecommerce_Backend.Interfaces.Admin
{
    public interface IAdminCategoryService
    {
        Task<int> AddCategoryAsync(AdminCreateCategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int categoryId, AdminUpdateCategoryDTO dto);
        Task<bool> DeleteCategoryAsync(int categoryId);
        Task<AdminCategoryDetailsDTO?> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<AdminCategoryDetailsDTO>> GetAllCategoriesAsync();
    }
}
