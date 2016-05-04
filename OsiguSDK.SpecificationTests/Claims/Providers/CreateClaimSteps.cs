using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CreateClaimSteps
    {
        private static IDictionary<string,string> CurrentScenario { get; set; } 

        private static void GenerateItemList()
        {
            Requests.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
        }

        private static void GenerateItemListWithSubstitutes(int id, string fixQuantity)
        {
            Requests.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
            Requests.CreateClaimRequest.Items[1].SubstituteProductId = ConstantElements.ProviderSubstituteProducts[id];

            switch (fixQuantity.ToLower())
            {
                case "lower":
                    Requests.CreateClaimRequest.Items[1].Quantity *= 0.5m;
                    break;
                case "higher":
                    Requests.CreateClaimRequest.Items[1].Quantity *= 5m;
                    break;
                case "same":
                    Requests.CreateClaimRequest.Items[1].Quantity = 1m;
                    break;
            }
        }

        private static void FillItemList()
        {
            var r = new Random();
            for (var i = 0; i < 3; i++)
            {
                Requests.CreateClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = r.Next(100, 10000)/100m,
                    ProductId = ConstantElements.ProviderAssociateProductId[i],
                    Quantity = (r.Next(0, 1000)%10) + 1
                });
            }
        }

        [Given(@"I have the provider claims client without authorization")]
        public void GivenIHaveTheProviderClaimsClientWithoutAuthorization()
        {
            Responses.ErrorId = 0;
            TestClients.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = ConfigurationClients.ConfigProviderBranch1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider claims client without valid slug")]
        public void GivenIHaveTheProviderClaimsClientWithoutValidSlug()
        {
            Responses.ErrorId = 0;
            TestClients.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigProviderBranch1.Authentication
            });
        }

        [Given(@"I have the provider claims client")]
        public void GivenIHaveTheProviderClaimsClient()
        {
            Responses.ErrorId = 0;
            TestClients.ClaimsProviderClient = new ClaimsClient(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have the provider claims client two")]
        public void GivenIHaveTheProviderClaimsClientTwo()
        {
            TestClients.ClaimsProviderClientWithNoPermission = new ClaimsClient(ConfigurationClients.ConfigProviderBranch2);
        }

        [When(@"I request the create a claim endpoint with the second client")]
        public void WhenIRequestTheCreateAClaimEndpointWithTheSecondClient()
        {
            Responses.Authorization.Pin.Should().NotBeNullOrEmpty("The authorization was not compleated correctly");
            try
            {
                TestClients.ClaimsProviderClientWithNoPermission.CreateClaim(Responses.Authorization.Id, Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request")]
        public void GivenTheCreateAClaimRequest()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
        }

        [When(@"the create a claim request")]
        public void WhenTheCreateAClaimRequest()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
            Requests.CreateClaimRequest.Pin = Responses.Authorization.Pin;
        }


        [When(@"I request the create a claim endpoint")]
        public void WhenIRequestTheCreateAClaimEndpoint()
        {
            Responses.Authorization.Pin.Should().NotBeNullOrEmpty("The authorization was not compleated correctly");
            try
            {
                Responses.QueueId = TestClients.ClaimsProviderClient.CreateClaim(Responses.Authorization.Id, Requests.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request with an unexisting authorization")]
        public void GivenTheCreateAClaimRequestWithAnUnexistingAuthorization()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            Responses.Authorization.Id = "NotExistingAuth";
            Responses.Authorization.Pin = "PIN";
        }

        [Then(@"the result should be not existing")]
        public void ThenTheResultShouldBeNotExisting()
        {
            Responses.ErrorId.Should().Be(404);
        }

        [Given(@"the create a claim request with an authorization not associated with the provider")]
        public void GivenTheCreateAClaimRequestWithAnAuthorizationNotAssociatedWithTheProvider()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();

            Responses.Authorization.Pin = "PIN";
            Responses.Authorization.Id = "NotAssociated";
        }

        [Given(@"the create a claim request with an invalid pin")]
        public void GivenTheCreateAClaimRequestWithAnInvalidPin()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
        }

        [Given(@"the create a claim request with missing fields")]
        public void GivenTheCreateAClaimRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];

            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
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
            Responses.ErrorId.Should().Be(422);
        }

        [When(@"the create a claim request with a product that does not exists in osigu products")]
        public void WhenTheCreateAClaimRequestWithAProductThatDoesNotExistsInOsiguProducts()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Responses.Authorization.Pin;
            GenerateItemList();

            Requests.CreateClaimRequest.Items.First().ProductId = "NotExistingOsigu";
        }

        [When(@"the create a claim request with different products")]
        public void WhenTheCreateAClaimRequestWithDifferentProducts()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Responses.Authorization.Pin;
            GenerateItemList();
        }

        [When(@"the create a claim request with repeated products")]
        public void WhenTheCreateAClaimRequestWithRepeatedProducts()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Responses.Authorization.Pin;
            GenerateItemList();
            FillItemList();
        }

        [When(@"the create a claim request with substitute products")]
        public void WhenTheCreateAClaimRequestWithSubstituteProducts(Table scenario)
        {
            CurrentScenario = scenario.Rows.First();
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = Responses.Authorization.Pin;
            GenerateItemListWithSubstitutes(int.Parse(CurrentScenario["ItemId"]), CurrentScenario["FixQuantity"]);
        }

        [Then(@"the result should be the expected")]
        public void ThenTheResultShouldBeTheExpected()
        {
            Responses.ErrorId.Should().Be(int.Parse(CurrentScenario["ExpectedResult"]));
        }

    }
}
