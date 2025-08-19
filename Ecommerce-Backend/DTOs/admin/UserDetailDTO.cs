using Ecommerce_Backend.DTOs.Cart;
using System.Collections.Generic;

namespace Ecommerce_Backend.DTOs.Admin
{
    public class UserDetailDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public bool IsBlocked { get; set; }
        public string Role { get; set; } = "";
        public IEnumerable<AdminUserOrderDTO> Orders { get; set; } = new List<AdminUserOrderDTO>();
        public IEnumerable<AdminUserAddressDTO> Addresses { get; set; } = new List<AdminUserAddressDTO>();
        public IEnumerable<AdminUserCartItemDTO> CartItems { get; set; } = new List<AdminUserCartItemDTO>();
    }
}
