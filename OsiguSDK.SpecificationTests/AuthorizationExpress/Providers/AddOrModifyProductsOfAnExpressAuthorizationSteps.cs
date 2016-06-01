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
        public AddOrModifyProductsOfAnExpressAuthorizationSteps()
        {
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
            CurrentData.ExpressAutorizationItems = new List<AddOrModifyItemsExpressAuthorization.Item>();
        }

        [Given(@"I have entered a valid authorization id")]
        public void GivenIHaveEnteredAValidAuthorizationId()
        {
            Responses.QueueId = CreateExpressAuthorization();
            Responses.ExpressAuthorizationId = CheckExpressAuthorizationStatus(Responses.QueueId);
        }

        [Given(@"I have entered many valid products")]
        public void GivenIHaveEnteredManyValidProducts()
        {
            CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.GenerateProducts(2);
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
            CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.GenerateProducts(1);
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
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.GenerateProducts(1);

            CurrentData.ExpressAutorizationItems[0].Quantity = 0;
        }
        
        [Given(@"I have entered one product with negative quantity")]
        public void GivenIHaveEnteredOneProductWithNegativeQuantity()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.GenerateProducts(1);

            CurrentData.ExpressAutorizationItems[0].Quantity = -1;
        }
        
        [Given(@"I have entered one product with price equal to cero")]
        public void GivenIHaveEnteredOneProductWithPriceEqualToCero()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.GenerateProducts(1);

            CurrentData.ExpressAutorizationItems[0].Price = 0m;
        }

        [Given(@"I have entered one product with negative price")]
        public void GivenIHaveEnteredOneProductWithNegativePrice()
        {
            if (!CurrentData.ExpressAutorizationItems.Any())
                CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.GenerateProducts(1);

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
            ValidateExpressAuthorizationResponse();
        }

      

        [Then(@"the result should be the express authorization with the product approved")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithTheProductApproved()
        {
            
        }
        
        [Then(@"the result should be the express authorization with all valid products approved")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithAllValidProductsApproved()
        {
            ValidateExpressAuthorizationResponse();
        }
        
        [Then(@"the result should be the express authorization with all products denied")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithAllProductsDenied()
        {
            
        }
        
        [Then(@"the result should be the express authorization with the product denied")]
        public void ThenTheResultShouldBeTheExpressAuthorizationWithTheProductDenied()
        {
            
        }

        private string CreateExpressAuthorization()
        {
            string queuiId = null;
            var request = new CreateExpressAuthorizationRequest
            {
                InsurerId = ConstantElements.InsurerId.ToString(),
                PolicyHolder = ConstantElements.PolicyHolder
            };

            Utils.Dump("CreateExpressAuthorizationRequest: ", request);

            try
            {
                queuiId = ExpressAuthorizationTool.CreateExpressAuthorization(request);
                Utils.Dump("QueueId: ", Responses.QueueId);

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            return queuiId;
        }

        private string CheckExpressAuthorizationStatus(string queueId)
        {
            Responses.ErrorId = 0;
            string expressAuthorizationId = null;
            try
            {
                expressAuthorizationId = ExpressAuthorizationTool.CheckQueueStatus(queueId);

                Utils.Dump("CheckExpressAuthorizationStatus: ", expressAuthorizationId);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            return expressAuthorizationId;
        }

        private static void ValidateExpressAuthorizationResponse()
        {
            var coInsurancePercentage = Responses.ExpressAuthorization.Items.Average(x => x.CoInsurancePercentage) / 100;

            Responses.ExpressAuthorization.Items.ShouldAllBeEquivalentTo(CurrentData.ExpressAutorizationItems,
                x => x.Excluding(y => y.CoInsurancePercentage));

            Responses.ExpressAuthorization.Id.Should().Be(Responses.ExpressAuthorizationId);
            Responses.ExpressAuthorization.InsurerName.Should().Be(ConstantElements.InsurerName);
            Responses.ExpressAuthorization.PolicyHolder.ShouldBeEquivalentTo(ConstantElements.PolicyHolder);
            Responses.ExpressAuthorization.Copayment.Should().Be(0);
            Responses.ExpressAuthorization.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 30000);
            Responses.ExpressAuthorization.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, 30000);

            //TODO: Remove the first round, the API must round it
            Math.Round(Responses.ExpressAuthorization.TotalCoInsurance, 2)
                .Should()
                .Be(Math.Round(CurrentData.ExpressAutorizationItems.Sum(x => x.Quantity * x.Price) * coInsurancePercentage, 2));
            //Responses.ExpressAuthorization.TotalCoInsurance.Should().Be(CurrentData.ExpressAutorizationItems.Sum(x => x.Quantity * x.Price) * coInsurancePercentage);
        }
    }
}
