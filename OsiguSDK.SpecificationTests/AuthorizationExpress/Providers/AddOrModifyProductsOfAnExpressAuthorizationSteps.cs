using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class AddOrModifyProductsOfAnExpressAuthorizationSteps
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }
        public ExpressAuthorizationHelper ExpressAuthorizationHelper { get; set; }

        public AddOrModifyProductsOfAnExpressAuthorizationSteps()
        {
            ExpressAuthorizationHelper = new ExpressAuthorizationHelper();
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
            CurrentData.ExpressAutorizationItems = new List<AddOrModifyItemsExpressAuthorization.Item>();

        }

        [Given(@"I have entered a valid authorization id")]
        public void GivenIHaveEnteredAValidAuthorizationId()
        {
            Responses.QueueId = ExpressAuthorizationHelper.CreateExpressAuthorization();
            Responses.ExpressAuthorizationId = ExpressAuthorizationHelper.CheckExpressAuthorizationStatus(Responses.QueueId);
        }

        [Given(@"I have entered many valid products")]
        public void GivenIHaveEnteredManyValidProducts()
        {
            CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(2);
        }
        
        [Given(@"I have the request data for add items of an express authorization")]
        public void GivenIHaveTheRequestDataForAddItemsOfAnExpressAuthorization()
        {
            Requests.AddOrModifyItemsExpressAuthorizationRequest = new AddOrModifyItemsExpressAuthorization
            {
                Items = CurrentData.ExpressAutorizationItems
            };

            Utils.Dump("AddOrModifyItemsExpressAuthorizationRequest  Sent: ", Requests.AddOrModifyItemsExpressAuthorizationRequest);
        }
        
        [Given(@"I have entered one valid product")]
        public void GivenIHaveEnteredOneValidProduct()
        {
            CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(1);
        }
        
        [Given(@"I have entered many invalid products")]
        public void GivenIHaveEnteredManyInvalidProducts()
        {
            CurrentData.ExpressAutorizationItems.AddRange(new List<AddOrModifyItemsExpressAuthorization.Item>
            {
                new AddOrModifyItemsExpressAuthorization.Item
                {
                    ProductId = "InvalidProduct1",
                    Price = 1m,
                    Quantity = 1
                },
                new AddOrModifyItemsExpressAuthorization.Item
                {
                    ProductId = "InvalidProduct2",
                    Price = 1m,
                    Quantity = 1
                },
                new AddOrModifyItemsExpressAuthorization.Item
                {
                    ProductId = "InvalidProduct3",
                    Price = 1m,
                    Quantity = 1
                }
            });
        }
        
        [Given(@"I have entered one invalid product")]
        public void GivenIHaveEnteredOneInvalidProduct()
        {
            CurrentData.ExpressAutorizationItems.AddRange(new List<AddOrModifyItemsExpressAuthorization.Item>
            {
                new AddOrModifyItemsExpressAuthorization.Item
                {
                    ProductId = "InvalidProduct1",
                    Price = 1m,
                    Quantity = 1
                },
            });
        }

        [Given(@"I have entered one non-existent product")]
        public void GivenIHaveEnteredOneNon_ExistentProduct()
        {
            CurrentData.ExpressAutorizationItems.AddRange(new List<AddOrModifyItemsExpressAuthorization.Item>
            {
                new AddOrModifyItemsExpressAuthorization.Item
                {
                    ProductId = "NonExistentProduct1",
                    Price = 1m,
                    Quantity = 1
                },
            });
        }


        [Given(@"I have entered one product with quantity equal to cero")]
        public void GivenIHaveEnteredOneProductWithQuantityEqualToCero()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(1);

            CurrentData.ExpressAutorizationItems[0].Quantity = 0;
        }
        
        [Given(@"I have entered one product with negative quantity")]
        public void GivenIHaveEnteredOneProductWithNegativeQuantity()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(1);

            CurrentData.ExpressAutorizationItems[0].Quantity = -1;
        }
        
        [Given(@"I have entered one product with price equal to cero")]
        public void GivenIHaveEnteredOneProductWithPriceEqualToCero()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(1);

            CurrentData.ExpressAutorizationItems[0].Price = 0m;
        }

        [Given(@"I have entered one product with negative price")]
        public void GivenIHaveEnteredOneProductWithNegativePrice()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(1);

            CurrentData.ExpressAutorizationItems[0].Price = -15m;
        }


        [When(@"I make the add items of an express authorization request to the endpoint")]
        public void WhenIMakeTheAddItemsOfAnExpressAuthorizationRequestToTheEndpoint()
        {
            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    TestClients.ExpressAuthorizationClient.AddOrModifyItemsExpressAuthorization(
                        Responses.ExpressAuthorizationId, Requests.AddOrModifyItemsExpressAuthorizationRequest);

                Utils.Dump("ExpressAuthorization Response: ", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("AddOrModifyItemsExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }

        }
        
        [Then(@"the result should be the express authorization with all products approved")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithAllProductsApproved()
        {
           ExpressAuthorizationHelper.ValidateExpressAuthorizationResponse();
        }

      

        [Then(@"the result should be the express authorization with the product approved")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithTheProductApproved()
        {
            
        }
        
        [Then(@"the result should be the express authorization with all valid products approved")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithAllValidProductsApproved()
        {
            ExpressAuthorizationHelper.ValidateExpressAuthorizationResponse();
        }
        
        [Then(@"the result should be the express authorization with all products denied")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithAllProductsDenied()
        {
            
        }
        
        [Then(@"the result should be the express authorization with the product denied")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithTheProductDenied()
        {
            
        }
        
        
    }
}
