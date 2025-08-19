using Ecommerce_Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Required]
    public int AddressId { get; set; }

    [ForeignKey(nameof(AddressId))]
    public Address Address { get; set; }

    [Required]
    public DateTime OrderDate { get; set; }

    [Required, MaxLength(20)]
    public string Status { get; set; }  // pending, shipped, delivered, cancelled

    // Navigation property
    public ICollection<OrderItem> OrderItems { get; set; }
}