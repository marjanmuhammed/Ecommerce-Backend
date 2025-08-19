
using Ecommerce_Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Address
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    [Required, MaxLength(150)]
    public string FullName { get; set; }

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; }

    [Required, MaxLength(20)]
    public string PhoneNumber { get; set; }

    [Required, MaxLength(500)]
    public string AddressLine { get; set; }

    [Required, MaxLength(10)]
    public string Pincode { get; set; }
}
