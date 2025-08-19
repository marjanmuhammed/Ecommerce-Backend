using Ecommerce_Backend.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_Backend.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        // Navigation
        public ICollection<Product> Products { get; set; }
    }
}
