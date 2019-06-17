using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Models
{
    public class ProductModel
    {
        public string Id{get;set;}
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal CostPerUnit { get; set; }
    }
}
