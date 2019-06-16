using CustomerBasketProject.Abstract;
using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Concrete
{
    public class BasketContainer : IBasketContainer
    {
        //mocked method that returns empty basket 
        public BasketModel GetCustomerBasket()
        {
            return new BasketModel
            {
                BasketContent = new List<BasketEntryModel> ()
            };
        }
    }
}
