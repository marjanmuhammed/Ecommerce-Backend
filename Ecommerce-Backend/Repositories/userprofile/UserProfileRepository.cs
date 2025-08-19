using Ecommerce_Backend.Data;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Ecommerce_Backend.Repositories.userprofile;
using Microsoft.EntityFrameworkCore;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly ApplicationDbContext _context;

    public UserProfileRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.users.FirstOrDefaultAsync(u => u.EmailAddress == email);
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.users.FindAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
