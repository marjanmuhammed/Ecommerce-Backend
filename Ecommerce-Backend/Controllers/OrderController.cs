using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));

        var order = new Order
        {
            UserId = userId,
            AddressId = dto.AddressId,
            OrderDate = DateTime.UtcNow,
            Status = "pending",
            OrderItems = dto.OrderItems.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList()
        };

        var created = await _orderService.CreateOrderAsync(order);

        if (created)
            return Ok(new { message = "Order placed successfully" });
        else
            return BadRequest(new { message = "Failed to place order" });
    }

    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));
        var orders = await _orderService.GetOrdersForUserAsync(userId);
        return Ok(orders);
    }

    // Add this to get specific order details for logged-in user
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));
        var order = await _orderService.GetOrderByIdAsync(orderId, userId);

        if (order == null)
            return NotFound(new { message = "Order not found or access denied" });

        return Ok(order);
    }
}
