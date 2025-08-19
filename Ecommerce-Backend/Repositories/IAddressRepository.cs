using Ecommerce_Backend.Models;
using System.Threading.Tasks;

public interface IAddressRepository
{
    Task AddAsync(Address address);
    Task<Address> GetByIdAsync(int addressId);
    void Update(Address address);
    void Delete(Address address);
    Task SaveChangesAsync();
}
