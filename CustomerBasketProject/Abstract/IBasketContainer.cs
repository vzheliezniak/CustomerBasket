using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Abstract
{
    public interface IBasketContainer
    {
        BasketModel GetCustomerBasket();
    }
}
