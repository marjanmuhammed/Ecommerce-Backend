using AutoMapper;
using Ecommerce_Backend.DTOs;
using Ecommerce_Backend.Models;
using Ecommerce_Backend.Repositories;
using System;
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

        // ✅ Fixed CreateOrderAsync with null checks and proper OrderItems mapping
        public async Task<OrderResponseDto> CreateOrderAsync(Order order)
        {
            try
            {
                if (order == null) throw new ArgumentNullException(nameof(order));
                if (order.OrderItems == null || order.OrderItems.Count == 0)
                    throw new ArgumentException("Order must have at least one item.");

                order.OrderDate = DateTime.Now;
                order.Status = "Pending"; // default status

                await _orderRepository.AddOrderAsync(order);
                await _orderRepository.SaveChangesAsync();

                // return mapped response DTO
                return _mapper.Map<OrderResponseDto>(order);
            }
            catch (Exception ex)
            {
                // Log the exact error for debugging
                Console.WriteLine("Error creating order: " + ex.Message);
                throw; // rethrow to let controller return 500 with message
            }
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

            _orderRepository.DeleteOrder(order);  // Make sure repo method exists
            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
