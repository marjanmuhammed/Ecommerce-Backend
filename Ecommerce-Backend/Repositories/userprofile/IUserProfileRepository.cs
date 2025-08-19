using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Repositories.userprofile
{
    public interface IUserProfileRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
