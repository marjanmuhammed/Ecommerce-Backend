using Ecommerce_Backend.Helpers;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Hosting;
using Ecommerce_Backend.Helpers;
using Ecommerce_Backend.Models;
using MyEcomApi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IWebHostEnvironment _env;

        public ProductService(IProductRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Product> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Product> CreateAsync(Product p)
        {
            await _repo.AddAsync(p);
            await _repo.SaveChangesAsync();
            return p;
        }

        public async Task<bool> UpdateAsync(Product p)
        {
            _repo.Update(p);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return false;
            // delete image file
            FileHelper.DeleteImageIfExists(_env.WebRootPath, existing.ImageUrl);
            _repo.Remove(existing);
            return await _repo.SaveChangesAsync();
        }
    }
}
