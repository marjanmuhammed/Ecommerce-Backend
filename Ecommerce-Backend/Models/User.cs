using CloudinaryDotNet.Actions;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Ecommerce_Backend.Models


{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }


        [Required]
        public string PasswordHash { get; set; }


        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public string Role { get; set; } = "user"; // default role

        public ICollection<CartItem> CartItems { get; set; }

        public bool IsBlocked { get; set; } = false;

        public ICollection<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }

    }
}
