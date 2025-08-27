using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Controllers
{
    [ApiController]
    [Route("api/admin/orders")]
    [Authorize(Roles = "Admin")]
    public class AdminOrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public AdminOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string status)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(orderId, status);
            if (updated)
                return Ok(new { message = "Order status updated" });
            else
                return BadRequest(new { message = "Failed to update order status" });
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var deleted = await _orderService.DeleteOrderAsync(orderId);
            if (deleted)
                return Ok(new { message = "Order deleted successfully" });
            else
                return NotFound(new { message = "Order not found" });
        }
        ///new//
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        { 
            var orders = await _orderService.GetOrdersForUserAsync(userId);
            if (orders == null || orders.Count == 0)
                return NotFound(new { message = "No orders found for this user" });

            return Ok(orders);
        }


    }
}
