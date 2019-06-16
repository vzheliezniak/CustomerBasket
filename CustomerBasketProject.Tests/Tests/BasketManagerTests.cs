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
        Mock<IBasketContainer> customerBasket = new Mock<IBasketContainer>();

        [Test]
        public void AddItemToBasket_NullItemPassed_ArgumentNullExceptionIsThrown()
        {
            customerBasket.Setup(x => x.GetCustomerBasket()).Returns(BasketAssets.exampleEmptyBasket);
            IBasketManager basketManager = new BasketManager(customerBasket.Object);
            Assert.Throws<ArgumentNullException>(() => { basketManager.AddItemToBasket(null); });
        }

        [Test]
        public void AddItemToBasket_ItemIsAddedToEmptyBasket_BasketHasOneItem()
        {            
            IBasketManager basketManager = Setup(BasketAssets.exampleEmptyBasket);
            var resultBasket = basketManager.AddItemToBasket(ProductAssets.exampleProductShuriken);

            Assert.AreEqual(resultBasket.BasketContent.Count, 1);
        }

        [Test]
        public void AddItemToBasket_ItemsIsAddedToBasketWithProducts_QuantityIncreased()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);
            var resultBasket = basketManager.AddItemToBasket(ProductAssets.exampleProductBagOfPogs);

            int expectedQuantity = 2;
            Assert.AreEqual(resultBasket.BasketContent[0].Quantity, expectedQuantity);
        }

        [Test]
        public void RemoveItem_BasketIsEmpty_NothingChanges()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            var result = basketManager.Remove(ProductAssets.exampleProductBagOfPogs.Id, true);
            Assert.AreSame(result, initialBasket);
        } 

        [Test]
        public void RemoveItem_ItemDoesNotExistInBusket_ArgumentExceptionIsThrown()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            Assert.Throws<ArgumentException>(() => {
                basketManager.Remove(ProductAssets.exampleProductPaperMask.Id, false);
            });
        }

        [Test]
        public void RemoveItem_ItemExistsInBusket_QuantityDescreased()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            var result = basketManager.Remove(ProductAssets.exampleProductShuriken.Id, false);

            Assert.Less(result.BasketContent[1].Quantity, initialBasket.BasketContent[1].Quantity);
        }

        [Test]
        public void RemoveWholeSet_ItemExists_WholeSetOfItemsIsRemoved()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            var result = basketManager.Remove(ProductAssets.exampleProductShuriken.Id, true);

            Assert.IsEmpty(result.BasketContent.Where(x => x.Product.Id == ProductAssets.exampleProductShuriken.Id));
        }

        [Test]
        public void EmptyBusket_EmptyBusketIsReturned()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            var result = basketManager.EmptyBasket();

            Assert.IsEmpty(result.BasketContent);
        }

        [Test]
        public void GetSubTotalOfItems_ItemDoesNotExistInBusket_ArgumentExceptionIsThrown()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            Assert.Throws<ArgumentException>(() => { basketManager.GetSubPrice(ProductAssets.exampleProductPaperMask.Id); });
        }

        [Test]
        public void GetSubTotalOfItems_ItemsExists_SubPriceReturned()
        {
            var initialBasket = BasketAssets.exampleBasketWithContent;
            IBasketManager basketManager = Setup(initialBasket);

            var subShurikensPrice = basketManager.GetSubPrice(ProductAssets.exampleProductShuriken.Id);

            Assert.AreEqual(subShurikensPrice, initialBasket.BasketContent[1].Product.CostPerUnit * initialBasket.BasketContent[1].Quantity);
        }

        [Test]
        public void GetSubTotalOfTheBasket_BasketIsEmpty_ZeroPriceIsReturned()
        {
            var initialBasket = BasketAssets.exampleEmptyBasket;
            IBasketManager basketManager = Setup(initialBasket);

            var subBasketPrice = basketManager.GetSubBasketPrice();
            Assert.AreEqual(subBasketPrice, 0.0m);
        }

        private IBasketManager Setup(BasketModel basket)
        {
            customerBasket.Setup(x => x.GetCustomerBasket()).Returns(basket);
            return new BasketManager(customerBasket.Object);
        }
    }
}
