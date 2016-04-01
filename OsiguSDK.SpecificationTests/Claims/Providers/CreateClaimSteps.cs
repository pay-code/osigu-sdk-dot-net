using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CreateClaimSteps
    {
        private static void GenerateItemList()
        {
            Tools.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
        }

        private static void FillItemList()
        {
            var r = new Random();
            for (var i = 0; i < 3; i++)
            {
                Tools.CreateClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = r.Next()*10,
                    ProductId = Tools.ProviderAssociateProductId[i],
                    Quantity = (r.Next(0, 1000)%10)
                });
            }
        }

        [Given(@"I have the provider claims client without authorization")]
        public void GivenIHaveTheProviderClaimsClientWithoutAuthorization()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = Tools.ConfigProviderBranch1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider claims client without valid slug")]
        public void GivenIHaveTheProviderClaimsClientWithoutValidSlug()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigProviderBranch1Development.Authentication
            });
        }

        [Given(@"I have the provider claims client")]
        public void GivenIHaveTheProviderClaimsClient()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(Tools.ConfigProviderBranch1Development);
        }

        [Given(@"I have the provider claims client two")]
        public void GivenIHaveTheProviderClaimsClientTwo()
        {
            Tools.ClaimsProviderClientWithNoPermission = new ClaimsClient(Tools.ConfigProviderBranch2Development);
        }

        [When(@"I request the create a claim endpoint with the second client")]
        public void WhenIRequestTheCreateAClaimEndpointWithTheSecondClient()
        {
            try
            {
                Tools.ClaimsProviderClientWithNoPermission.CreateClaim(Tools.AuthorizationId, Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request")]
        public void GivenTheCreateAClaimRequest()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
        }

        [When(@"I request the create a claim endpoint")]
        public void WhenIRequestTheCreateAClaimEndpoint()
        {
            try
            {
                Tools.ClaimsProviderClient.CreateClaim(Tools.AuthorizationId, Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request with an unexisting authorization")]
        public void GivenTheCreateAClaimRequestWithAnUnexistingAuthorization()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            Tools.AuthorizationId = "NotExistingAuth";
        }

        [Then(@"the result should be not existing")]
        public void ThenTheResultShouldBeNotExisting()
        {
            Tools.ErrorId.Should().Be(404);
        }

        [Given(@"the create a claim request with an authorization not associated with the provider")]
        public void GivenTheCreateAClaimRequestWithAnAuthorizationNotAssociatedWithTheProvider()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
            //TODO: Not associated but existing
            Tools.AuthorizationId = "NotAssociated";
        }

        [Given(@"the create a claim request with an invalid pin")]
        public void GivenTheCreateAClaimRequestWithAnInvalidPin()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
        }

        [Given(@"the create a claim request with missing fields")]
        public void GivenTheCreateAClaimRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];

            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            switch (missingField)
            {
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

        }

        [Then(@"the result should be unprossesable entity")]
        public void ThenTheResultShouldBeUnprossesableEntity()
        {
            Tools.ErrorId.Should().Be(422);
        }

        [Given(@"the create a claim request with a product that does not exists in osigu products")]
        public void GivenTheCreateAClaimRequestWithAProductThatDoesNotExistsInOsiguProducts()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            //TODO: Not existing in osigu
            Tools.CreateClaimRequest.Items.First().ProductId = "NotExistingOsigu";
        }

        [When(@"the create a claim request with different products")]
        public void WhenTheCreateAClaimRequestWithDifferentProducts()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Tools.CreateClaimRequest.Pin = Tools.PIN;
            GenerateItemList();
        }

        [When(@"the create a claim request with repeated products")]
        public void WhenTheCreateAClaimRequestWithRepeatedProducts()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Tools.CreateClaimRequest.Pin = Tools.PIN;
            GenerateItemList();
            FillItemList();
        }
    }
}
