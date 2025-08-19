﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ecommerce_Backend.Models;

namespace Ecommerce_Backend.DTOs
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
