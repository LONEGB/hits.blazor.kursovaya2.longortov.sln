﻿namespace TovanyUchetV2.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Category { get; set; } = "";

        public decimal Price { get; set; }

        public int QuantityInStock { get; set; }
    }
}
