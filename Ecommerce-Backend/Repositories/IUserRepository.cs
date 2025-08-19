using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAddressAsync(string emailAddress);

        Task<User> GetByRefreshTokenAsync(string refreshToken);


        Task AddUserAsync(User user);

        Task SaveChangesAsync();


      

    }
}
