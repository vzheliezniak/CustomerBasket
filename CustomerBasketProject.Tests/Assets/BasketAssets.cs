using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Tests.Assets
{
    public static class BasketAssets
    {
        public static BasketModel exampleEmptyBasket = new BasketModel { BasketContent = new List<BasketEntryModel>() };

        public static BasketModel exampleBasketWithContent = new BasketModel
        {
            BasketContent = new List<BasketEntryModel>
            {
                new BasketEntryModel{ Product = ProductAssets.exampleProductBagOfPogs, Quantity = 2}, 
                new BasketEntryModel{ Product = ProductAssets.exampleProductShuriken, Quantity = 3}
            }
        };
    }
}
