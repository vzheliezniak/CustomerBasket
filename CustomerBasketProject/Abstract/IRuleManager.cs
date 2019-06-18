using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Abstract
{
    public interface IRuleManager
    {
        List<DiscountModel> GetApplicableDiscounts(BasketModel basket);
        decimal ApplyDiscounts(BasketModel basket);
    }
}
