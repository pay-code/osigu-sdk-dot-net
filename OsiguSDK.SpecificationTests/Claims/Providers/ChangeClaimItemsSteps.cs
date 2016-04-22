using System;
using System.Collections.Generic;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models.Requests;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class ChangeClaimItemsSteps
    {
        private static void ChangeItems(bool unexisting = false)
        {
            var r = new Random();

            Tools.CreateClaimRequest = new CreateClaimRequest
            {
                Pin = Tools.PIN,
                Items = new List<CreateClaimRequest.Item>
                {
                    new CreateClaimRequest.Item
                    {
                        Price = r.Next(100, 10000)/100m,
                        ProductId = !unexisting ? Tools.ProviderAssociateProductId[(r.Next(0, 999)%3)] : "productId",
                        Quantity = (r.Next(0, 1000)%10) + 1
                    }
                }
            };
        }


        [When(@"I request the change items request")]
        public void WhenIRequestTheChangeItemsRequest()
        {
            ChangeItems();
            try
            {
                Tools.ClaimsProviderClient.ChangeClaimItems(Tools.Claim.Id.ToString(), Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with an invalid claim id")]
        public void WhenIRequestTheChangeItemsRequestWithAnInvalidClaimId()
        {
            ChangeItems();
            try
            {
                Tools.ClaimsProviderClient.ChangeClaimItems("0", Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with an invalid PIN")]
        public void WhenIRequestTheChangeItemsRequestWithAnInvalidPIN()
        {
            ChangeItems();
            try
            {
                Tools.CreateClaimRequest.Pin = "InvalidPin";
                Tools.ClaimsProviderClient.ChangeClaimItems(Tools.Claim.Id.ToString(), Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with missing fields")]
        public void WhenIRequestTheChangeItemsRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];
            ChangeItems();

            switch (missingField)
            {
                case "ClaimId":
                    Tools.Claim.Id = 0;
                    break;
                case "PIN":
                    Tools.CreateClaimRequest.Pin = "";
                    break;
                case "Empty Items":
                    Tools.CreateClaimRequest.Items = null;
                    break;
                case "Empty Items 2":
                    Tools.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
                    break;
                case "Product Id":
                    Tools.CreateClaimRequest.Items.First().ProductId = string.Empty;
                    break;
                case "Price":
                    Tools.CreateClaimRequest.Items.First().Price = 0;
                    break;
                case "Quantity":
                    Tools.CreateClaimRequest.Items.First().Quantity = 0;
                    break;
                default:
                    ScenarioContext.Current.Pending();
                    break;
            }

            try
            {
                Tools.ClaimsProviderClient.ChangeClaimItems(Tools.Claim.Id.ToString(), Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with unexisting osigu product")]
        public void WhenIRequestTheChangeItemsRequestWithUnexistingOsiguProduct()
        {
            ChangeItems(true);

            try
            {
                Tools.ClaimsProviderClient.ChangeClaimItems(Tools.Claim.Id.ToString(), Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

    }
}
