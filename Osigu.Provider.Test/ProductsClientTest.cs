using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests.v1;
using Assert = NUnit.Framework.Assert;

namespace Osigu.Provider.Test
{
    [TestClass]
    public class ProductsClientTest
    {
        [TestMethod]
        public void ShouldSubmitProduct()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var productsClient = new ProductsClient(config);

            var product = new SubmitProductRequest()
            {
                FullName = "NEXIUM 100MG PRUEBAS UNIT_TESTING",
                Manufacturer = "ROEMMERS",
                Name = "NEXIUM 100MG PRUEBAS UNIT_TESTING",
                ProductId = "201803271_DM",
                Type = "Drug"
            };

                productsClient.SubmitProduct(product);
                Assert.IsTrue(true);


        }

        [TestMethod]
        public void ShouldSubmitRemoval()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var productsClient = new ProductsClient(config);

                productsClient.SubmitRemoval("201803271_DM");
                Assert.IsTrue(true);
        }

        [TestMethod]
        public void ShouldGetSingleProduct()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var productsClient = new ProductsClient(config);
            var product = new Product();

                product = productsClient.GetSingleProduct("132124");
                Assert.IsNotNull(product);
        }

        [TestMethod]
        public void ShouldGetListOfProducts()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var productsClient = new ProductsClient(config);
            var products = new Pagination<Product>();

                products = productsClient.GetListOfProducts();
                
                Assert.IsNull(products);


        }
    }
}
