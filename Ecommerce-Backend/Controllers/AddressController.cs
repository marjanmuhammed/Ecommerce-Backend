using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/address")]
[Authorize]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] AddressDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));

        var address = new Address
        {
            UserId = userId,
            FullName = dto.FullName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            AddressLine = dto.AddressLine,
            Pincode = dto.Pincode
        };

        var id = await _addressService.AddAddressAsync(address);
        return Ok(new { AddressId = id, message = "Address added successfully" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddress(int id)
    {
        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null)
            return NotFound();

        var userId = int.Parse(User.FindFirstValue("UserId"));
        if (address.UserId != userId)
            return Unauthorized();

        return Ok(address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDTO dto)
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));

        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null)
            return NotFound();

        if (address.UserId != userId)
            return Unauthorized();

        address.FullName = dto.FullName;
        address.Email = dto.Email;
        address.PhoneNumber = dto.PhoneNumber;
        address.AddressLine = dto.AddressLine;
        address.Pincode = dto.Pincode;

        var updated = await _addressService.UpdateAddressAsync(address);
        if (!updated)
            return BadRequest();

        return Ok(new { message = "Address updated successfully" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));

        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null)
            return NotFound();

        if (address.UserId != userId)
            return Unauthorized();

        var deleted = await _addressService.DeleteAddressAsync(id);
        if (!deleted)
            return BadRequest();

        return Ok(new { message = "Address deleted successfully" });
    }
}
