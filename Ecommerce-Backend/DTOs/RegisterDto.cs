using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Backend.DTOs
{
    public class RegisterDto
    {

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }


        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }


        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }


        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

    }
}
