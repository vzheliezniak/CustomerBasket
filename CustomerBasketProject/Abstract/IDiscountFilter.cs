using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Abstract
{
    public interface IDiscountFilter
    {
        bool CheckRuleCondition(BasketModel basket, string operatorSymbol, string productId, int amount);
    }
}
