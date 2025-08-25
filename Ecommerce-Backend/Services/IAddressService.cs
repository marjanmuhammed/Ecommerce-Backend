using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.Services
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllByUserIdAsync(int userId);
        Task<Address?> GetByIdAsync(int id);
        Task<Address?> GetByIdAndUserIdAsync(int id, int userId);
        Task<Address> AddAsync(Address address);
        Task<Address?> UpdateAsync(Address address);
        Task<bool> DeleteAsync(int id, int userId);
       

    }
}