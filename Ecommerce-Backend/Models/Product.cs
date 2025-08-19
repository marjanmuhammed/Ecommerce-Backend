using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // image path or url
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        // FK
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<CartItem> CartItems { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }

    
    }
}
