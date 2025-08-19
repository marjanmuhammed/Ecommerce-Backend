using Ecommerce_Backend.Models;
using System.Threading.Tasks;

public interface IAddressService
{
    Task<int> AddAddressAsync(Address address);
    Task<Address> GetAddressByIdAsync(int addressId);
    Task<bool> UpdateAddressAsync(Address address);
    Task<bool> DeleteAddressAsync(int addressId);
}
