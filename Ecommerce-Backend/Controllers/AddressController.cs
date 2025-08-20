using System.Security.Claims;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }

        // Helpers: Entity <-> DTO mapping
        private static AddressDTO ToDto(Address a) => new AddressDTO
        {
            Id = a.Id,
            FullName = a.FullName,
            Email = a.Email,
            PhoneNumber = a.PhoneNumber,
            AddressLine = a.AddressLine,
            Pincode = a.Pincode,
            UserId = a.UserId
        };

        private static Address FromDto(AddressDTO d) => new Address
        {
            Id = d.Id,
            FullName = d.FullName,
            Email = d.Email,
            PhoneNumber = d.PhoneNumber,
            AddressLine = d.AddressLine,
            Pincode = d.Pincode,
            UserId = d.UserId
        };

        // GET: /api/address
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAll()
        {
            var addresses = await _service.GetAllAsync();
            return Ok(addresses.Select(ToDto));
        }

        // GET: /api/address/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AddressDTO>> GetById(int id)
        {
            var address = await _service.GetByIdAsync(id);
            if (address == null) return NotFound();
            return Ok(ToDto(address));
        }

        // POST: /api/address
        [HttpPost]
        public async Task<ActionResult<AddressDTO>> Create([FromBody] AddressDTO dto)
        {
            // 👇 pull UserId from JWT claim
            var userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized("UserId claim missing in token");

            dto.UserId = int.Parse(userIdClaim);

            var created = await _service.AddAsync(FromDto(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToDto(created));
        }

        // PUT: /api/address/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AddressDTO>> Update(int id, [FromBody] AddressDTO dto)
        {
            if (id != dto.Id) return BadRequest("Id mismatch");

            var updated = await _service.UpdateAsync(FromDto(dto));
            if (updated == null) return NotFound();

            return Ok(ToDto(updated));
        }

        // DELETE: /api/address/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
