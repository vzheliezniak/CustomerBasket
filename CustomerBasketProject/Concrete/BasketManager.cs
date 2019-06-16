using CustomerBasketProject.Abstract;
using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Concrete
{
    public class BasketManager: IBasketManager
    {
        private readonly IBasketContainer _basketContainer;
        public BasketManager(IBasketContainer basketContainer)
        {
            _basketContainer = basketContainer;
        }

        public BasketModel AddItemToBasket(ProductModel product)
        {
            throw new NotImplementedException();
        }

        public string Display()
        {
            throw new NotImplementedException();
        }

        public BasketModel EmptyBasket()
        {
            throw new NotImplementedException();
        }

        public BasketModel Remove(string productId, bool shouldRemoveAllItems)
        {
            throw new NotImplementedException();
        }

        public decimal GetSubPrice(string productId)
        {
            throw new NotImplementedException();
        }

        public decimal GetSubBasketPrice()
        {
            throw new NotImplementedException();
        }

        public decimal GetGrandTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
