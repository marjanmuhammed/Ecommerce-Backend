using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;

namespace Ecommerce_Backend.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;

        public AddressService(IAddressRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Address>> GetAllByUserIdAsync(int userId) =>
            _repository.GetAllByUserIdAsync(userId);

        public Task<Address?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);

        public Task<Address?> GetByIdAndUserIdAsync(int id, int userId) =>
            _repository.GetByIdAndUserIdAsync(id, userId);

        public Task<Address> AddAsync(Address address) => _repository.AddAsync(address);

        public Task<Address?> UpdateAsync(Address address) => _repository.UpdateAsync(address);

        public Task<bool> DeleteAsync(int id, int userId) => _repository.DeleteAsync(id, userId);
    }
}