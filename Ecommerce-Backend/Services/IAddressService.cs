using Ecommerce_Backend.Models;
using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<Address> AddAsync(Address address);
        Task<Address?> UpdateAsync(Address address);
        Task<bool> DeleteAsync(int id);
    }
}
