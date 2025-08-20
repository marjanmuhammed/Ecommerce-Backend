using AutoMapper;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _svc;
        private readonly IMapper _mapper;

        public ProductsController(IProductService svc, IMapper mapper)
        {
            _svc = svc;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _svc.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<ActionResult<ProductDto>> Get(int id)
        {
            var product = await _svc.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(_mapper.Map<ProductDto>(product));
        }

        // optional: filter by category
        [HttpGet("category/{categoryName}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategory(string categoryName)
        {
            var all = await _svc.GetAllAsync();
            var filtered = all.Where(p => p.Category.Name.Equals(categoryName, StringComparison.OrdinalIgnoreCase));
            return Ok(_mapper.Map<IEnumerable<ProductDto>>(filtered));
        }
    }
}
