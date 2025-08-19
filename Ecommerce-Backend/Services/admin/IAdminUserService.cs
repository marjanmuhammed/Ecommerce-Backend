using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.Admin;

namespace Ecommerce_Backend.Interfaces.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<UserDetailDTO>> GetAllUsersAsync();
        Task<UserDetailDTO?> GetUserByIdAsync(int id);
        Task<bool> BlockUserAsync(int id);
        Task<bool> UnblockUserAsync(int id);

        Task<bool> DeleteUserAsync(int id);
    }
}
