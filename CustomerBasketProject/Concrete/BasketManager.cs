using CustomerBasketProject.Abstract;
using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Concrete
{
    public class BasketManager: IBasketManager
    {
        private BasketModel _customerBasket;
        public BasketManager(BasketModel basket)
        {
            _customerBasket = basket;
        }
    }
}
