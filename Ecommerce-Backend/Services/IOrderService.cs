using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderService
{
    Task<bool> CreateOrderAsync(Order order);
    Task<List<OrderResponseDto>> GetOrdersForUserAsync(int userId);

    // If needed, vere methodsum add cheyyu
    Task<OrderResponseDto> GetOrderByIdAsync(int orderId, int? userId = null);
    Task<List<OrderResponseDto>> GetAllOrdersAsync();
    Task<bool> UpdateOrderStatusAsync(int orderId, string status);

    Task<bool> DeleteOrderAsync(int orderId);

}
