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

        // Helper to get current user ID
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirstValue("UserId");
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("UserId claim missing in token");

            return int.Parse(userIdClaim);
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

        // GET: /api/address - Now returns only current user's addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressDTO>>> GetAll()
        {
            try
            {
                var userId = GetCurrentUserId();
                var addresses = await _service.GetAllByUserIdAsync(userId);
                return Ok(addresses.Select(ToDto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // GET: /api/address/{id} - Now validates user ownership
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AddressDTO>> GetById(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var address = await _service.GetByIdAndUserIdAsync(id, userId);
                if (address == null) return NotFound();
                return Ok(ToDto(address));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // POST: /api/address
        [HttpPost]
        public async Task<ActionResult<AddressDTO>> Create([FromBody] AddressDTO dto)
        {
            try
            {
                var userId = GetCurrentUserId();
                dto.UserId = userId;

                var created = await _service.AddAsync(FromDto(dto));
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, ToDto(created));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // PUT: /api/address/{id} - Now validates user ownership
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AddressDTO>> Update(int id, [FromBody] AddressDTO dto)
        {
            try
            {
                if (id != dto.Id) return BadRequest("Id mismatch");

                var userId = GetCurrentUserId();
                if (dto.UserId != userId) return Forbid();

                var updated = await _service.UpdateAsync(FromDto(dto));
                if (updated == null) return NotFound();

                return Ok(ToDto(updated));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // DELETE: /api/address/{id} - Now validates user ownership
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userId = GetCurrentUserId();
                var result = await _service.DeleteAsync(id, userId);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}