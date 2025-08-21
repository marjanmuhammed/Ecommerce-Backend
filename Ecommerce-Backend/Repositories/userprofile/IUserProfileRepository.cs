using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Repositories
{
    public interface IUserProfileRepository
    {
        Task<User?> GetByEmailAddressAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task SaveChangesAsync();
        // Add other methods as needed
    }
}