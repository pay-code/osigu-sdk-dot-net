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
            Requests.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
        }

        private static void FillItemList()
        {
            var r = new Random();
            for (var i = 0; i < 3; i++)
            {
                Requests.CreateClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = r.Next(100, 10000)/100m,
                    ProductId = Tools.ProviderAssociateProductId[i],
                    Quantity = (r.Next(0, 1000)%10) + 1
                });
            }
        }

        [Given(@"I have the provider claims client without authorization")]
        public void GivenIHaveTheProviderClaimsClientWithoutAuthorization()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = ConfigurationClients.ConfigProviderBranch1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider claims client without valid slug")]
        public void GivenIHaveTheProviderClaimsClientWithoutValidSlug()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigProviderBranch1.Authentication
            });
        }

        [Given(@"I have the provider claims client")]
        public void GivenIHaveTheProviderClaimsClient()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have the provider claims client two")]
        public void GivenIHaveTheProviderClaimsClientTwo()
        {
            Tools.ClaimsProviderClientWithNoPermission = new ClaimsClient(ConfigurationClients.ConfigProviderBranch2);
        }

        [When(@"I request the create a claim endpoint with the second client")]
        public void WhenIRequestTheCreateAClaimEndpointWithTheSecondClient()
        {
            Tools.PIN.Should().NotBeNullOrEmpty("The authorization was not compleated correctly");
            try
            {
                Tools.ClaimsProviderClientWithNoPermission.CreateClaim(Tools.AuthorizationId, Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request")]
        public void GivenTheCreateAClaimRequest()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
        }

        [When(@"the create a claim request")]
        public void WhenTheCreateAClaimRequest()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
            Requests.CreateClaimRequest.Pin = Tools.PIN;
        }


        [When(@"I request the create a claim endpoint")]
        public void WhenIRequestTheCreateAClaimEndpoint()
        {
            Tools.PIN.Should().NotBeNullOrEmpty("The authorization was not compleated correctly");
            try
            {
                Tools.QueueId = Tools.ClaimsProviderClient.CreateClaim(Tools.AuthorizationId, Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request with an unexisting authorization")]
        public void GivenTheCreateAClaimRequestWithAnUnexistingAuthorization()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            Tools.AuthorizationId = "NotExistingAuth";
            Tools.PIN = "PIN";
        }

        [Then(@"the result should be not existing")]
        public void ThenTheResultShouldBeNotExisting()
        {
            Tools.ErrorId.Should().Be(404);
        }

        [Given(@"the create a claim request with an authorization not associated with the provider")]
        public void GivenTheCreateAClaimRequestWithAnAuthorizationNotAssociatedWithTheProvider()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            Tools.PIN = "PIN";
            Tools.AuthorizationId = "NotAssociated";
        }

        [Given(@"the create a claim request with an invalid pin")]
        public void GivenTheCreateAClaimRequestWithAnInvalidPin()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
        }

        [Given(@"the create a claim request with missing fields")]
        public void GivenTheCreateAClaimRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];

            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            switch (missingField)
            {
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

        }

        [Then(@"the result should be unprossesable entity")]
        public void ThenTheResultShouldBeUnprossesableEntity()
        {
            Tools.ErrorId.Should().Be(422);
        }

        [When(@"the create a claim request with a product that does not exists in osigu products")]
        public void WhenTheCreateAClaimRequestWithAProductThatDoesNotExistsInOsiguProducts()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Tools.PIN;
            GenerateItemList();

            Requests.CreateClaimRequest.Items.First().ProductId = "NotExistingOsigu";
        }

        [When(@"the create a claim request with different products")]
        public void WhenTheCreateAClaimRequestWithDifferentProducts()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Tools.PIN;
            GenerateItemList();
        }

        [When(@"the create a claim request with repeated products")]
        public void WhenTheCreateAClaimRequestWithRepeatedProducts()
        {
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Tools.PIN;
            GenerateItemList();
            FillItemList();
        }
    }
}
