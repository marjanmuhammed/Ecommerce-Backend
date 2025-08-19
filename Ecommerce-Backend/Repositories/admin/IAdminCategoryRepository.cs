using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.admin;
using Ecommerce_Backend.DTOs.Admin;

namespace Ecommerce_Backend.Interfaces.Admin
{
    public interface IAdminCategoryRepository
    {
        Task<int> AddCategoryAsync(AdminCreateCategoryDTO dto);
        Task<bool> UpdateCategoryAsync(int categoryId, AdminUpdateCategoryDTO dto);
        Task<bool> DeleteCategoryAsync(int categoryId);

        // Ippo ithu add cheyyanam
        Task<AdminCategoryDetailsDTO?> GetCategoryByIdAsync(int categoryId);
        Task<IEnumerable<AdminCategoryDetailsDTO>> GetAllCategoriesAsync();
    }
}
