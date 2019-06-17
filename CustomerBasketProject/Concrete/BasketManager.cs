using CustomerBasketProject.Abstract;
using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            if(product == null)
            {
                throw new ArgumentNullException();
            }

            var currentBasket = _basketContainer.GetCustomerBasket();
            var existingItemInBasket = currentBasket.BasketContent.Find(x => x.Product.Id == product.Id);
            
            if(existingItemInBasket == null)
            {
                var newBasketEntry = new BasketEntryModel { Product = product, Quantity = 1, SubTotal = product.CostPerUnit };
                currentBasket.BasketContent.Add(newBasketEntry);
            }
            else
            {
                existingItemInBasket.Quantity++;
                existingItemInBasket.SubTotal += product.CostPerUnit;
            }

            currentBasket.SubBasketPrice = GetSubBasketPrice();
            //apply discounts
            //calculate grand total price
            //currentBasket.GrandTotalPrice = GetGrandTotalPrice();

            return currentBasket;
        }

        public string Display()
        {
            throw new NotImplementedException();
        }

        public BasketModel EmptyBasket()
        {
            var currentBasket = _basketContainer.GetCustomerBasket();
            currentBasket.BasketContent.Clear();
            currentBasket.GrandTotalPrice = 0.0m;
            currentBasket.SubBasketPrice = 0.0m;
            return currentBasket;
        }

        public BasketModel Remove(string productId, bool shouldRemoveAllItems)
        {
            var currentBasket = _basketContainer.GetCustomerBasket();
            if(currentBasket.BasketContent.Count == 0)
            {
                return currentBasket;
            }

            var existingItemInBasket = currentBasket.BasketContent.Find(x => x.Product.Id == productId);
            if(existingItemInBasket == null)
            {
                throw new ArgumentException();
            }
            var updatedQuantity = existingItemInBasket.Quantity - 1;
            if(shouldRemoveAllItems || updatedQuantity == 0)
            {
                currentBasket.BasketContent.Remove(existingItemInBasket);
            }
            else
            {
                existingItemInBasket.Quantity--;
                existingItemInBasket.SubTotal = GetSubPrice(productId); 
            }

            currentBasket.SubBasketPrice = GetSubBasketPrice();
            //recheck discounts
            //calculate grand total price
            //currentBasket.GrandTotalPrice = GetGrandTotalPrice();

            return currentBasket;

        }

        public decimal GetSubPrice(string productId)
        {
            var currentBasket = _basketContainer.GetCustomerBasket();
            var basketEntry = currentBasket.BasketContent.Find(x => x.Product.Id == productId);
            if(basketEntry == null)
            {
                throw new ArgumentException();
            }

            return basketEntry.Product.CostPerUnit * basketEntry.Quantity;
        }

        public decimal GetSubBasketPrice()
        {
            var currentBasket = _basketContainer.GetCustomerBasket();
            var res = currentBasket.BasketContent.Sum(x => x.SubTotal);
            return res;
        }

        public decimal GetGrandTotalPrice()
        {
            throw new NotImplementedException();
        }
    }
}
