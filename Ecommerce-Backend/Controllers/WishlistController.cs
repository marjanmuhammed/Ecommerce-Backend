using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    [Authorize(Roles = "User")]
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
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var wishlist = await _wishlistService.GetWishlistByUserIdAsync(userId);
            return Ok(wishlist);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] int productId)
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var added = await _wishlistService.AddToWishlistAsync(userId, productId);
            if (added)
                return Ok(new { message = "Added to wishlist" });
            else
                return BadRequest(new { message = "Product already in wishlist" });
        }

        [HttpDelete("{wishlistItemId}")]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistItemId)
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));
            var removed = await _wishlistService.RemoveFromWishlistAsync(userId, wishlistItemId);
            if (removed)
                return Ok(new { message = "Removed from wishlist" });
            else
                return BadRequest(new { message = "Failed to remove from wishlist" });
        }
    }
}
