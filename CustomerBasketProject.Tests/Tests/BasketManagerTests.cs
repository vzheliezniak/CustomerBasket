using CustomerBasketProject.Abstract;
using CustomerBasketProject.Concrete;
using CustomerBasketProject.Models;
using CustomerBasketProject.Tests.Assets;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomerBasketProject.Tests.Tests
{
    [TestFixture]
    public class BasketManagerTests
    {
        [Test]
        public void AddItemToBasket_NullItemPassed_ArgumentNullExceptionIsThrown()
        {
            IBasketManager basketManager = SetupEmptyBasket();
            Assert.Throws<ArgumentNullException>(() => { basketManager.AddItemToBasket(null); });
        }

        [Test]
        public void AddItemToBasket_ItemIsAddedToEmptyBasket_BasketHasOneItem()
        {
            IBasketManager basketManager = SetupEmptyBasket();
            var resultBasket = basketManager.AddItemToBasket(ProductAssets.exampleProductShuriken);

            Assert.AreEqual(resultBasket.BasketContent.Count, 1);
        }

        [Test]
        public void AddItemToBasket_ItemsIsAddedToBasketWithProducts_QuantityIncreased()
        {
            var initialBasket = new BasketModel();
            var basketManager = SetupBasketWithProducts(initialBasket);
            int expectedQuantity = initialBasket.BasketContent[0].Quantity + 1;
            var resultBasket = basketManager.AddItemToBasket(ProductAssets.exampleProductBagOfPogs);
            
            Assert.AreEqual(resultBasket.BasketContent[0].Quantity, expectedQuantity);
        }

        [Test]
        public void RemoveItem_BasketIsEmpty_NothingChanges()
        {
            IBasketManager basketManager = SetupEmptyBasket();

            var result = basketManager.Remove(ProductAssets.exampleProductBagOfPogs.Id, true);
            Assert.IsEmpty(result.BasketContent);
        } 

        [Test]
        public void RemoveItem_ItemDoesNotExistInBusket_ArgumentExceptionIsThrown()
        {
            var initialBasket = new BasketModel();
            var basketManager = SetupBasketWithProducts(initialBasket);

            Assert.Throws<ArgumentException>(() => {
                basketManager.Remove(ProductAssets.exampleProductPaperMask.Id, false);
            });
        }

        [Test]
        public void RemoveItem_ItemExistsInBusket_QuantityDescreased()
        {
            var initialBasket = new BasketModel();
            var basketManager = SetupBasketWithProducts(initialBasket);
            var initialProductQuantity = initialBasket.BasketContent[1].Quantity;
            var result = basketManager.Remove(ProductAssets.exampleProductShuriken.Id, false);

            Assert.Less(result.BasketContent[1].Quantity, initialProductQuantity);
        }

        [Test]
        public void RemoveWholeSet_ItemExists_WholeSetOfItemsIsRemoved()
        {
            var initialBasket = new BasketModel();
            var basketManager = SetupBasketWithProducts(initialBasket);

            var testProduct = ProductAssets.exampleProductBagOfPogs;

            var result = basketManager.Remove(testProduct.Id, true);

            Assert.IsEmpty(result.BasketContent.Where(x => x.Product.Id == testProduct.Id));
        }

        [Test]
        public void EmptyBusket_EmptyBusketIsReturned()
        {
            IBasketManager basketManager = SetupEmptyBasket();

            var result = basketManager.EmptyBasket();

            Assert.IsEmpty(result.BasketContent);
        }

        [Test]
        public void GetSubTotalOfItems_ItemDoesNotExistInBusket_ArgumentExceptionIsThrown()
        {
            var initialBasket = new BasketModel();
            var basketManager = SetupBasketWithProducts(initialBasket);

            Assert.Throws<ArgumentException>(() => { basketManager.GetSubPrice(ProductAssets.exampleProductPaperMask.Id); });
        }

        [Test]
        public void GetSubTotalOfItems_ItemsExists_SubPriceReturned()
        {
            var initialBasket = new BasketModel();
            var basketManager = SetupBasketWithProducts(initialBasket);

            var subShurikensPrice = basketManager.GetSubPrice(ProductAssets.exampleProductShuriken.Id);

            Assert.AreEqual(subShurikensPrice, initialBasket.BasketContent[1].Product.CostPerUnit * initialBasket.BasketContent[1].Quantity);
        }

        [Test]
        public void GetSubTotalOfTheBasket_BasketIsEmpty_ZeroPriceIsReturned()
        {
            IBasketManager basketManager = SetupEmptyBasket();

            var subBasketPrice = basketManager.GetSubBasketPrice();
            Assert.AreEqual(subBasketPrice, 0.0m);
        }

        private IBasketManager SetupEmptyBasket()
        {
            //mock empty basket model
            BasketModel basketModel = new BasketModel { BasketContent = new List<BasketEntryModel>() };
            
            //mock BasketContainer service
            Mock<IBasketContainer> customerBasket = new Mock<IBasketContainer>();
            customerBasket.Setup(x => x.GetCustomerBasket()).Returns(basketModel);
            return new BasketManager(customerBasket.Object);
        }

        private IBasketManager SetupBasketWithProducts(BasketModel basketModel)
        {
            //initialize some basket entries
            var shurikensProducts = new BasketEntryModel { Product = ProductAssets.exampleProductShuriken, Quantity = 3 };
            var pogProducts = new BasketEntryModel { Product = ProductAssets.exampleProductBagOfPogs, Quantity = 2 };
            
            //mock basket model with products
            basketModel.BasketContent = new List<BasketEntryModel> { pogProducts, shurikensProducts } ;

            //mock BasketContainer service
            Mock<IBasketContainer> customerBasket = new Mock<IBasketContainer>();
            customerBasket.Setup(x => x.GetCustomerBasket()).Returns(basketModel);
            return new BasketManager(customerBasket.Object);
        }
    }
}
