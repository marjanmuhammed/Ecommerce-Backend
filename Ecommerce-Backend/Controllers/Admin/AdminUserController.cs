using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce_Backend.DTOs.Admin;
using Ecommerce_Backend.Interfaces.Admin;

namespace Ecommerce_Backend.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/[controller]")]
    [Authorize(Roles = "Admin")]  // Admin-only access
    public class AdminUsersController : ControllerBase
    {
        private readonly IAdminUserService _userService;

        public AdminUsersController(IAdminUserService userService)
        {
            _userService = userService;
        }

        // GET: api/admin/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetailDTO>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/admin/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        // PUT: api/admin/users/block/{id}
        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            var result = await _userService.BlockUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // PUT: api/admin/users/unblock/{id}
        [HttpPut("unblock/{id}")]
        public async Task<IActionResult> UnblockUser(int id)
        {
            var result = await _userService.UnblockUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/admin/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
