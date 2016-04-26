using System;
using System.Collections.Generic;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class ChangeClaimItemsSteps
    {
        private static void ChangeItems(bool unexisting = false)
        {
            var r = new Random();

            Requests.CreateClaimRequest = new CreateClaimRequest
            {
                Pin = Responses.Authorization.Pin,
                Items = new List<CreateClaimRequest.Item>
                {
                    new CreateClaimRequest.Item
                    {
                        Price = r.Next(100, 10000)/100m,
                        ProductId = !unexisting ? ConstantElements.ProviderAssociateProductId[(r.Next(0, 999)%3)] : "productId",
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
                TestClients.ClaimsProviderClient.ChangeClaimItems(Responses.Claim.Id.ToString(), Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with an invalid claim id")]
        public void WhenIRequestTheChangeItemsRequestWithAnInvalidClaimId()
        {
            ChangeItems();
            try
            {
                TestClients.ClaimsProviderClient.ChangeClaimItems("0", Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with an invalid PIN")]
        public void WhenIRequestTheChangeItemsRequestWithAnInvalidPIN()
        {
            ChangeItems();
            try
            {
                Requests.CreateClaimRequest.Pin = "InvalidPin";
                TestClients.ClaimsProviderClient.ChangeClaimItems(Responses.Claim.Id.ToString(), Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
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
                    Responses.Claim.Id = 0;
                    break;
                case "PIN":
                    Requests.CreateClaimRequest.Pin = "";
                    break;
                case "Empty Items":
                    Requests.CreateClaimRequest.Items = null;
                    break;
                case "Empty Items 2":
                    Requests.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
                    break;
                case "Product Id":
                    Requests.CreateClaimRequest.Items.First().ProductId = string.Empty;
                    break;
                case "Price":
                    Requests.CreateClaimRequest.Items.First().Price = 0;
                    break;
                case "Quantity":
                    Requests.CreateClaimRequest.Items.First().Quantity = 0;
                    break;
                default:
                    ScenarioContext.Current.Pending();
                    break;
            }

            try
            {
                TestClients.ClaimsProviderClient.ChangeClaimItems(Responses.Claim.Id.ToString(), Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the change items request with unexisting osigu product")]
        public void WhenIRequestTheChangeItemsRequestWithUnexistingOsiguProduct()
        {
            ChangeItems(true);

            try
            {
                TestClients.ClaimsProviderClient.ChangeClaimItems(Responses.Claim.Id.ToString(), Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

    }
}
