using Ecommerce_Backend.DTOs.Cart;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // require authenticated user
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) => _cartService = cartService;

        // GET api/cart/user
        [HttpGet("user")]
        public async Task<IActionResult> GetUserCart()
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized(new { message = "Invalid token" });

            var items = await _cartService.GetUserCartAsync(userId.Value);
            return Ok(items); // 200
        }

        // POST api/cart
        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddCartItemDTO dto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized(new { message = "Invalid token" });

            if (dto == null) return BadRequest(new { message = "Invalid payload" });

            var (Success, Error, Item) = await _cartService.AddToCartAsync(userId.Value, dto);
            if (!Success) return BadRequest(new { message = Error });

            // return created resource
            return CreatedAtAction(nameof(GetUserCart), null, Item);
        }

        // PUT api/cart/{cartItemId}
        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, [FromBody] UpdateCartItemDTO dto)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized(new { message = "Invalid token" });

            if (dto == null) return BadRequest(new { message = "Invalid payload" });

            var (Success, Error) = await _cartService.UpdateQuantityAsync(userId.Value, cartItemId, dto.Quantity);
            if (!Success) return BadRequest(new { message = Error });

            return NoContent(); // 204
        }

        // DELETE api/cart/{cartItemId}
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var userId = GetUserIdFromClaims();
            if (userId == null) return Unauthorized(new { message = "Invalid token" });

            var (Success, Error) = await _cartService.RemoveAsync(userId.Value, cartItemId);
            if (!Success) return NotFound(new { message = Error });

            return NoContent(); // 204
        }

        private int? GetUserIdFromClaims()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(idStr, out var id)) return id;
            return null;
        }
    }
}
