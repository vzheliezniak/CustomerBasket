using CustomerBasketProject.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerBasketProject.Tests.Assets
{
    public static class ProductAssets
    {
        public static ProductModel exampleProductShuriken = new ProductModel {
            Id = "RP-5NS-DITB",
            ProductName = "Shurikens",
            Description = "5 pointed Shurikins made from stainless stell",
            CostPerUnit = 8.95m
        };

        public static ProductModel exampleProductBagOfPogs = new ProductModel {
            Id = "RP-25D-SITB",
            ProductName = "Bag of Pogs",
            Description = "25 Random pogs designs",
            CostPerUnit = 5.31m
        };

        public static ProductModel exampleProductLargeBowlOfTrifle = new ProductModel
        {
            Id = "RP-1TB-EITB",
            ProductName = "Large bowl of Trifle",
            Description = "Large collectors edition bowl of Trifle",
            CostPerUnit = 2.75m
        };

        public static ProductModel exampleProductPaperMask = new ProductModel
        {
            Id = "RP-RPM-FITB",
            ProductName = "Paper Mask",
            Description = "Randomly selected paper mask",
            CostPerUnit = 0.30m
        };
    }
}
