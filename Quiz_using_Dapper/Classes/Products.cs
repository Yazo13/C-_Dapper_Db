﻿namespace Quiz_using_Dapper.Classes
{
    internal class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
