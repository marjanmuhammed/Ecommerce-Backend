using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.Admin;

namespace Ecommerce_Backend.Interfaces.Admin
{
    public interface IAdminUserRepository
    {
        Task<IEnumerable<UserDetailDTO>> GetAllUsersAsync();
        Task<UserDetailDTO?> GetUserByIdAsync(int userId);
        Task<bool> BlockUserAsync(int userId);
        Task<bool> UnblockUserAsync(int userId);

        Task<bool> DeleteUserAsync(int id);

    }
}
