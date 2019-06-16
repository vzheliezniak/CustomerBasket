using CustomerBasketProject.Concrete;
using CustomerBasketProject.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Tests.Tests
{
    [TestFixture]
    public class BasketManagerTests
    {
        [Test]
        public void Creation_NullBasketIsPassed_ArgumentExceptionIsThrown()
        {
            BasketModel basket = null;
            Assert.Throws<ArgumentException>(() => { new BasketManager(basket); });
        }
    }
}
