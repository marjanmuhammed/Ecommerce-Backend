using Ecommerce_Backend.DTOs.userprofile;
using Ecommerce_Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfileService _userService;
    public UserProfileController(IUserProfileService userService)
    {
        _userService = userService;
    }

    // GET: api/UserProfile/profile
    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var profile = await _userService.GetUserProfileAsync(email);
        if (profile == null) return NotFound();

        return Ok(profile);
    }

    // PUT: api/UserProfile (update profile)
    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserProfileDto dto)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var updated = await _userService.UpdateUserProfileAsync(email, dto);
        if (!updated) return BadRequest("Failed to update profile");

        return NoContent();
    }

    // PUT: api/UserProfile/profile/changepassword
    [HttpPut("profile/changepassword")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        if (string.IsNullOrEmpty(email)) return Unauthorized();

        var changed = await _userService.ChangePasswordAsync(email, dto);
        if (!changed) return BadRequest("Current password is incorrect.");

        return NoContent();
    }
}
