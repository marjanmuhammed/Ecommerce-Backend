using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    [Authorize(Roles = "user")]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistService _wishlistService;

        public WishlistController(IWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWishlist()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(); // user claim illa

            var userId = int.Parse(userIdClaim);
            var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] WishlistRequest request)
        {
            // FIXED: use correct claim key
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var addedItem = await _wishlistService.AddToWishlistAsync(userId, request.ProductId);

            if (addedItem == null)
                return BadRequest(new { message = "Product already in wishlist" });

            return Ok(addedItem); // return full DTO
        }


        [HttpDelete("{wishlistItemId}")]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistItemId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var removed = await _wishlistService.RemoveFromWishlistAsync(userId, wishlistItemId);

            if (removed)
                return Ok(new { message = "Removed from wishlist" });
            else
                return BadRequest(new { message = "Failed to remove from wishlist" });
        }
    }
}
