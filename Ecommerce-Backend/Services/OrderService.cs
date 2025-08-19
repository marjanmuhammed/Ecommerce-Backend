using AutoMapper;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce_Backend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderResponseDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> GetOrderByIdAsync(int orderId, int? userId = null)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return null;

            if (userId.HasValue && order.UserId != userId.Value)
                return null;

            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<List<OrderResponseDto>> GetOrdersForUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<List<OrderResponseDto>>(orders);
        }

        public async Task<bool> CreateOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;

            order.Status = status;
            await _orderRepository.UpdateOrderAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null) return false;

            _orderRepository.DeleteOrder(order);  // Need to add this method in repo
            await _orderRepository.SaveChangesAsync();
            return true;
        }

    }

}
