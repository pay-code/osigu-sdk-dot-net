using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using TechTalk.SpecFlow;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using OsiguSDK.Core.Exceptions;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class SubmitProductSteps
    {

        [Given(@"I have the provider products client")]
        public void GivenIHaveTheProviderProductsClient()
        {
            Tools.ErrorId = 0;
            Tools.ErrorId2 = 0;
            Tools.ProductsProviderClient = new ProductsClient(Tools.ConfigProviderBranch1Development);
        }

        [Given(@"I have the provider products client without authorization")]
        public void GivenIHaveTheProviderProductsClientWithoutAuthorization()
        {
            Tools.ErrorId = 0;
            Tools.ProductsProviderClient = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = Tools.ConfigProviderBranch1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider products client without valid slug")]
        public void GivenIHaveTheProviderProductsClientWithoutValidSlug()
        {
            Tools.ErrorId = 0;
            Tools.ProductsProviderClient = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigProviderBranch1Development.Authentication
            });
        }


        [Given(@"the submit a product request")]
        public void GivenTheSubmitAProductRequest()
        {
            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);
        }

        [Given(@"the submit a product request with longer id")]
        public void GivenTheSubmitAProductRequestWithLongerId()
        {
            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
        }


        [When(@"I request the submit a product endpoint")]
        public void WhenIRequestTheSubmitAProductEndpoint()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }

        }

        [When(@"I request the submit a removal endpoint without permission")]
        public void WhenIRequestTheSubmitARemovalEndpointWithoutPermission()
        {
            try
            {
                Tools.ProductsProductsClientWithNoPermission.SubmitProduct(Tools.SubmitProductRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }


        [Then(@"the result should be unauthorized")]
        public void ThenTheResultShouldBeUnauthorized()
        {
            Tools.ErrorId.Should().Be(403);
        }

        [Then(@"the result should be no permission")]
        public void ThenTheResultShouldBeNoPermission()
        {
            Tools.ErrorId.Should().Be(404);
        }


        [When(@"I request the submit a product endpoint twice")]
        public void WhenIRequestTheSubmitAProductEndpointTwice()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);

            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                newRequest.ProductId = Tools.SubmitProductRequest.ProductId;
                Tools.ProductsProviderClient.SubmitProduct(newRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId2 = exception.ResponseCode;
            }
        }

        [Then(@"the result should be ok on the first")]
        public void ThenTheResultShouldBeOkOnTheFirst()
        {
            Tools.ErrorId.Should().Be(0);
        }

        [Then(@"the result should be ignored on the second")]
        public void ThenTheResultShouldBeIgnoredOnTheSecond()
        {
            Tools.ErrorId2.Should().Be(0);
        }

        [Given(@"the submit a product request with missing fields")]
        public void GivenTheSubmitAProductRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];

            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);

            switch (missingField)
            {
                case "ProductId":
                    Tools.SubmitProductRequest.ProductId = string.Empty;
                    break;
                case "Name":
                    Tools.SubmitProductRequest.Name = string.Empty;
                    break;
                case "Full Name":
                    Tools.SubmitProductRequest.FullName = string.Empty;
                    break;
                case "Manufacturer":
                    Tools.SubmitProductRequest.ProductId = string.Empty;
                    break;
                default:
                    ScenarioContext.Current.Pending();
                    break;
            }
        }

        [Then(@"the result should be missing field")]
        public void ThenTheResultShouldBeMissingField()
        {
            Tools.ErrorId.Should().Be(422);
        }

        [When(@"the product is removed")]
        public void WhenTheProductIsRemoved()
        {
            Tools.ProductsProviderClient.SubmitRemoval(Tools.SubmitProductRequest.ProductId);
        }

        [Then(@"the result should be ok")]
        public void ThenTheResultShouldBeOk()
        {
            Tools.ErrorId.Should().Be(0);
        }

        [Then(@"the result should be a validation error")]
        public void ThenTheResultShouldBeAValidationError()
        {
            Tools.ErrorId.Should().Be(422);
        }

    }
}