using Ecommerce_Backend.Repositories;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Data;
using Microsoft.EntityFrameworkCore;




namespace Ecommerce_Backend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByEmailAddressAsync(string email)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.EmailAddress == email);
        }

        public async Task AddUserAsync(User user)
        {
            await _context.users.AddAsync(user);
        }


        public async Task<User> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.users
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }




    }
}
