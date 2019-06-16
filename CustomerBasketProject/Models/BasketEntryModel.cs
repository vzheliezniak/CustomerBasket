using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Models
{
    public class BasketEntryModel
    {
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }
    }
}
