using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
    Task<List<Order>> GetOrdersByUserIdAsync(int userId);
    Task<Order> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetAllOrdersAsync();
    Task UpdateOrderAsync(Order order);
    Task SaveChangesAsync();

    void DeleteOrder(Order order);

}
