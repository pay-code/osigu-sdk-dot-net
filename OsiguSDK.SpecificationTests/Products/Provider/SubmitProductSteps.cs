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

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class SubmitProductSteps
    {

        [Given(@"I have the provider products client")]
        public void GivenIHaveTheProviderProductsClient()
        {
            Responses.ErrorId = 0;
            Responses.ErrorId2 = 0;
            TestClients.ProductsProviderClient = new ProductsClient(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have the provider products client without authorization")]
        public void GivenIHaveTheProviderProductsClientWithoutAuthorization()
        {
            Responses.ErrorId = 0;
            TestClients.ProductsProviderClient = new ProductsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = ConfigurationClients.ConfigProviderBranch1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider products client without valid slug")]
        public void GivenIHaveTheProviderProductsClientWithoutValidSlug()
        {
            Responses.ErrorId = 0;
            TestClients.ProductsProviderClient = new ProductsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigProviderBranch1.Authentication
            });
        }


        [Given(@"the submit a product request")]
        public void GivenTheSubmitAProductRequest()
        {
            Requests.SubmitProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
            Requests.SubmitProductRequest.ProductId = Requests.SubmitProductRequest.ProductId.Substring(0, 25);
        }

        [Given(@"the submit a product request with longer id")]
        public void GivenTheSubmitAProductRequestWithLongerId()
        {
            Requests.SubmitProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
        }


        [When(@"I request the submit a product endpoint")]
        public void WhenIRequestTheSubmitAProductEndpoint()
        {
            try
            {
                TestClients.ProductsProviderClient.SubmitProduct(Requests.SubmitProductRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

        }

        [Then(@"the result should be unauthorized")]
        public void ThenTheResultShouldBeUnauthorized()
        {
            Responses.ErrorId.Should().Be(403);
        }

        [Then(@"the result should be no permission")]
        public void ThenTheResultShouldBeNoPermission()
        {
            Responses.ErrorId.Should().Be(403);
        }


        [When(@"I request the submit a product endpoint twice")]
        public void WhenIRequestTheSubmitAProductEndpointTwice()
        {
            try
            {
                TestClients.ProductsProviderClient.SubmitProduct(Requests.SubmitProductRequest);

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            try
            {
                var newRequest = TestClients.Fixture.Create<SubmitProductRequest>();
                newRequest.ProductId = Requests.SubmitProductRequest.ProductId;
                TestClients.ProductsProviderClient.SubmitProduct(newRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId2 = exception.ResponseCode;
            }
        }

        [Then(@"the result should be ok on the first")]
        public void ThenTheResultShouldBeOkOnTheFirst()
        {
            Responses.ErrorId.Should().Be(0);
        }

        [Then(@"the result should be ignored on the second")]
        public void ThenTheResultShouldBeIgnoredOnTheSecond()
        {
            Responses.ErrorId2.Should().Be(0);
        }

        [Given(@"the submit a product request with missing fields")]
        public void GivenTheSubmitAProductRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];

            Requests.SubmitProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
            Requests.SubmitProductRequest.ProductId = Requests.SubmitProductRequest.ProductId.Substring(0, 25);

            switch (missingField)
            {
                case "ProductId":
                    Requests.SubmitProductRequest.ProductId = string.Empty;
                    break;
                case "Name":
                    Requests.SubmitProductRequest.Name = string.Empty;
                    break;
                case "Full Name":
                    Requests.SubmitProductRequest.FullName = string.Empty;
                    break;
                case "Manufacturer":
                    Requests.SubmitProductRequest.ProductId = string.Empty;
                    break;
                default:
                    ScenarioContext.Current.Pending();
                    break;
            }
        }

        [Then(@"the result should be missing field")]
        public void ThenTheResultShouldBeMissingField()
        {
            Responses.ErrorId.Should().Be(422);
        }

        [When(@"the product is removed")]
        public void WhenTheProductIsRemoved()
        {
            TestClients.ProductsProviderClient.SubmitRemoval(Requests.SubmitProductRequest.ProductId);
        }

        [Then(@"the result should be ok")]
        public void ThenTheResultShouldBeOk()
        {
            Responses.ErrorId.Should().Be(0);
        }

        [Then(@"the result should be a validation error")]
        public void ThenTheResultShouldBeAValidationError()
        {
            Responses.ErrorId.Should().Be(422);
        }

    }
}