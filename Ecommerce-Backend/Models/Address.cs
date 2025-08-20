using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_Backend.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        
        [Required]
        [Phone]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string AddressLine { get; set; }
        
        [Required]
        [MaxLength(10)]
        public string Pincode { get; set; }
        
        // Remove IsDefault property
        // public bool IsDefault { get; set; }
        
        // Navigation property
        public virtual User User { get; set; }
    }
}