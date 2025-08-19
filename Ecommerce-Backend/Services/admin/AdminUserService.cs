using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.Interfaces.Admin;
using Ecommerce_Backend.DTOs.Admin;

namespace Ecommerce_Backend.Services.Admin
{
    public class AdminUserService : IAdminUserService
    {
        private readonly IAdminUserRepository _repo;
        public AdminUserService(IAdminUserRepository repo) => _repo = repo;

        public Task<IEnumerable<UserDetailDTO>> GetAllUsersAsync() => _repo.GetAllUsersAsync();
        public Task<UserDetailDTO?> GetUserByIdAsync(int id) => _repo.GetUserByIdAsync(id);
        public Task<bool> BlockUserAsync(int id) => _repo.BlockUserAsync(id);
        public Task<bool> UnblockUserAsync(int id) => _repo.UnblockUserAsync(id);

        public Task<bool> DeleteUserAsync(int id) => _repo.DeleteUserAsync(id);
    }
}
