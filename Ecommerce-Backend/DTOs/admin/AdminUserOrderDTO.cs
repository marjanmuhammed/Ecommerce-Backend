using System;
using System.Collections.Generic;

namespace Ecommerce_Backend.DTOs.Admin
{
    public class AdminUserOrderDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = "";
        public IEnumerable<AdminUserOrderItemDTO> Items { get; set; } = new List<AdminUserOrderItemDTO>();
    }
}