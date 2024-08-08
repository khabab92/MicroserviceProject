﻿using System.ComponentModel.DataAnnotations;

namespace Services.ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Range(1,10000)]
        public double Price { get; set; }
        public string ProductDescription { get; set;}
        public string ProductCategory { get; set;}
        public string ImageUrl { get; set;}

    }
}
