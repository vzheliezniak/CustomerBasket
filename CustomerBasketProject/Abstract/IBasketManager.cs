using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Abstract
{
    public interface IBasketManager
    {
        BasketModel AddItemToBasket(ProductModel product);
        BasketModel Remove(string productId, bool shouldRemoveAllItems);
        BasketModel EmptyBasket();
        decimal GetSubPrice(string productId);
        decimal GetSubBasketPrice();
        decimal GetGrandTotalPrice();
        string Display();
    }
}
