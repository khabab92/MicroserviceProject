﻿using System.ComponentModel.DataAnnotations;

namespace Services.ProductAPI.Models.Dto
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public string ImageUrl { get; set; }
    }
}
