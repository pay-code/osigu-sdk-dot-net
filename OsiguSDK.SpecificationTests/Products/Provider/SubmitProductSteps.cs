using System;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using TechTalk.SpecFlow;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;


namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class SubmitProductSteps
    {
        private string errorMessage { get; set; }
        private string errorMessage2 { get; set; }

        [Given(@"I have the provider products client")]
        public void GivenIHaveTheProviderProductsClient()
        {
            Tools.ProductsProviderClient = new ProductsClient(Tools.ConfigProviderBranch1Development);
        }

        [Given(@"I have the provider products client without authorization")]
        public void GivenIHaveTheProviderProductsClientWithoutAuthorization()
        {
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

        [When(@"I request the submit a product endpoint")]
        public void WhenIRequestTheSubmitAProductEndpoint()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

        }

        [Then(@"the result should be unauthorized")]
        public void ThenTheResultShouldBeUnauthorized()
        {
            errorMessage.Should().Contain("Server failed to authenticate the request. Make sure the value of the Authorization header is formed correctly including the signature");
        }

        [Then(@"the result should be no permission")]
        public void ThenTheResultShouldBeNoPermission()
        {
            errorMessage.Should().Contain("You don’t have permission to access this resource");
        }


        [When(@"I request the submit a product endpoint twice")]
        public void WhenIRequestTheSubmitAProductEndpointTwice()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
                errorMessage = string.Empty;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                newRequest.ProductId = Tools.SubmitProductRequest.ProductId;
                Tools.ProductsProviderClient.SubmitProduct(newRequest);
            }
            catch (Exception exception)
            {
                errorMessage2 = exception.Message;
            }
        }

        [Then(@"the result should be ok on the first")]
        public void ThenTheResultShouldBeOkOnTheFirst()
        {
            errorMessage.Should().BeEmpty();
        }

        [Then(@"the result should be error on the second")]
        public void ThenTheResultShouldBeErrorOnTheSecond()
        {
            errorMessage2.Should().Contain("id");
        }

        [When(@"I request the submit a product endpoint twice with the same name")]
        public void WhenIRequestTheSubmitAProductEndpointTwiceWithTheSameName()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
                errorMessage = string.Empty;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);
                newRequest.Name = Tools.SubmitProductRequest.Name;
                Tools.ProductsProviderClient.SubmitProduct(newRequest);
            }
            catch (Exception exception)
            {
                errorMessage2 = exception.Message;
            }
        }


        [Then(@"the result should be error because of repeated name on the second")]
        public void ThenTheResultShouldBeErrorBecauseOfRepeatedNameOnTheSecond()
        {
            errorMessage2.Should().Contain("name");
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
            errorMessage.Should().Contain("missing");
        }

        [When(@"the product is removed")]
        public void WhenTheProductIsRemoved()
        {
            Tools.ProductsProviderClient.SubmitRemoval(Tools.SubmitProductRequest.ProductId);
        }

        [Then(@"the result should be ok")]
        public void ThenTheResultShouldBeOk()
        {
            errorMessage.Should().BeEmpty();
        }

    }
}
