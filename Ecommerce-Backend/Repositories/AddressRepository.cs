using Ecommerce_Backend.Data;
using Ecommerce_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AddressRepository : IAddressRepository
{
    private readonly ApplicationDbContext _context;

    public AddressRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
    }

    public async Task<Address> GetByIdAsync(int addressId)
    {
        return await _context.Addresses.FirstOrDefaultAsync(a => a.Id == addressId);
    }

    public void Update(Address address)
    {
        _context.Addresses.Update(address);
    }

    public void Delete(Address address)
    {
        _context.Addresses.Remove(address);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
