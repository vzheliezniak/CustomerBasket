using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Models
{
    public class RuleModel
    {
        public string RequiredProductId { get; set; }
        public int ProductAmount { get; set; }
        public string OperatorSymbol { get; set; }
        public DiscountModel Discount { get; set; }
    }
}
