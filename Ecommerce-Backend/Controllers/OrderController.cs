using Ecommerce_Backend.Data;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ApplicationDbContext _context;

    public OrderController(IOrderService orderService, ApplicationDbContext context)
    {
        _orderService = orderService;
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        try
        {
            var userId = int.Parse(User.FindFirstValue("UserId"));

            if (dto.OrderItems == null || !dto.OrderItems.Any())
                return BadRequest(new { message = "Order must have at least one item." });

            int addressId;

            if (dto.AddressId > 0)
            {
                // Use existing address
                var existingAddress = await _context.Addresses
                    .FirstOrDefaultAsync(a => a.Id == dto.AddressId && a.UserId == userId);
                if (existingAddress == null)
                    return BadRequest(new { message = "Invalid address." });

                addressId = existingAddress.Id;
            }
            else
            {
                // Create new address
                if (dto.Address == null)
                    return BadRequest(new { message = "Address information is required." });

                var newAddress = new Address
                {
                    UserId = userId,
                    FullName = dto.Address.FullName,
                    Email = dto.Address.Email,
                    PhoneNumber = dto.Address.PhoneNumber,
                    AddressLine = dto.Address.AddressLine,
                    Pincode = dto.Address.Pincode
                };

                _context.Addresses.Add(newAddress);
                await _context.SaveChangesAsync();

                addressId = newAddress.Id;
            }

            // Create order items
            var orderItems = new List<OrderItem>();
            foreach (var itemDto in dto.OrderItems)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);
                if (product == null)
                    return BadRequest(new { message = $"Product with ID {itemDto.ProductId} not found." });

                orderItems.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = itemDto.Quantity,
                    Price = product.Price
                });
            }

            var order = new Order
            {
                UserId = userId,
                AddressId = addressId,
                OrderDate = DateTime.UtcNow,
                Status = "pending",
                OrderItems = orderItems
            };

            var createdOrder = await _orderService.CreateOrderAsync(order);

            return Ok(new { message = "Order placed successfully", orderId = createdOrder.Id });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error placing order: " + ex.ToString());
            return StatusCode(500, new { message = "An unexpected error occurred." });
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));
        var orders = await _orderService.GetOrdersForUserAsync(userId);
        return Ok(orders);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(int orderId)
    {
        var userId = int.Parse(User.FindFirstValue("UserId"));
        var order = await _orderService.GetOrderByIdAsync(orderId, userId);

        if (order == null)
            return NotFound(new { message = "Order not found or access denied" });

        return Ok(order);
    }
    // DELETE api/orders/{orderId}
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        try
        {
            // Get logged-in user's ID from JWT
            var userId = int.Parse(User.FindFirstValue("UserId"));

            // Optional: fetch order first to check ownership
            var order = await _orderService.GetOrderByIdAsync(orderId, userId);
            if (order == null)
                return NotFound(new { message = "Order not found or access denied." });

            // Call service to delete order
            var deleted = await _orderService.DeleteOrderAsync(orderId);
            if (!deleted)
                return BadRequest(new { message = "Failed to delete order." });

            return Ok(new { message = "Order cancelled successfully." });
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error deleting order: " + ex);
            return StatusCode(500, new { message = "An unexpected error occurred." });
        }
    }

}


/////////////////////////////////////////////////////////////////////