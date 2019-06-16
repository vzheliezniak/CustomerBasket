using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Models
{
    public class BasketModel
    {
        public List<BasketEntryModel> BasketContent { get; set; }

        public decimal SubBasketPrice { get; set; }

        public decimal GrandTotalPrice { get; set; }
    }
}
