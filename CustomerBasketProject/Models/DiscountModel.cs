using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Models
{
    public enum DiscountType
    {
        OnProduct, 
        OnBasket
    }

    public enum DiscountAction
    {
        Percent, 
        FixedCoupon, 
        FreeGift
    }

    public class DiscountModel
    {
        public DiscountType Type { get; set; }
        public DiscountAction Action { get; set; }
        public string ProductId { get; set; }
        /// <summary>
        /// ???????
        /// </summary>
        public int Limitation { get; set; }
        
    }
}
