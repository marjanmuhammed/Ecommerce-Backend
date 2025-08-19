using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories; // assuming you have IAddressRepository
using System.Threading.Tasks;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    public async Task<int> AddAddressAsync(Address address)
    {
        await _addressRepository.AddAsync(address);
        await _addressRepository.SaveChangesAsync();
        return address.Id;
    }

    public async Task<Address> GetAddressByIdAsync(int addressId)
    {
        return await _addressRepository.GetByIdAsync(addressId);
    }

    public async Task<bool> UpdateAddressAsync(Address address)
    {
        var existing = await _addressRepository.GetByIdAsync(address.Id);
        if (existing == null) return false;

        existing.FullName = address.FullName;
        existing.Email = address.Email;
        existing.PhoneNumber = address.PhoneNumber;
        existing.AddressLine = address.AddressLine;
        existing.Pincode = address.Pincode;

        _addressRepository.Update(existing);
        await _addressRepository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAddressAsync(int addressId)
    {
        var address = await _addressRepository.GetByIdAsync(addressId);
        if (address == null) return false;

        _addressRepository.Delete(address);
        await _addressRepository.SaveChangesAsync();
        return true;
    }
}
