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
        private ProductsClient _client { get; set; }
        private SubmitProductRequest _request { get; set; }

        private string errorMessage { get; set; }
        private string errorMessage2 { get; set; }

        [Given(@"I have the provider products client")]
        public void GivenIHaveTheProviderProductsClient()
        {
            _client = new ProductsClient(Tools.ConfigProviderBranch1Development);
        }

        [Given(@"I have the provider products client without authorization")]
        public void GivenIHaveTheProviderProductsClientWithoutAuthorization()
        {
            _client = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = Tools.ConfigProviderBranch1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider products client without valid slug")]
        public void GivenIHaveTheProviderProductsClientWithoutValidSlug()
        {
            _client = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigProviderBranch1Development.Authentication
            });
        }


        [Given(@"the submit a product request")]
        public void GivenTheSubmitAProductRequest()
        {
            _request = Tools.Fixture.Create<SubmitProductRequest>();
            _request.ProductId = _request.ProductId.Substring(0, 25);
        }

        [When(@"I request the submit a product endpoint")]
        public void WhenIRequestTheSubmitAProductEndpoint()
        {
            try
            {
                _client.SubmitProduct(_request);
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
                _client.SubmitProduct(_request);
                errorMessage = string.Empty;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                newRequest.ProductId = _request.ProductId;
                _client.SubmitProduct(newRequest);
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
                _client.SubmitProduct(_request);
                errorMessage = string.Empty;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                _request.ProductId = _request.ProductId.Substring(0, 25);
                newRequest.Name = _request.Name;
                _client.SubmitProduct(newRequest);
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

            _request = Tools.Fixture.Create<SubmitProductRequest>();
            _request.ProductId = _request.ProductId.Substring(0, 25);

            switch (missingField)
            {
                case "ProductId":
                    _request.ProductId = string.Empty;
                    break;
                case "Name":
                    _request.Name = string.Empty;
                    break;
                case "Full Name":
                    _request.FullName = string.Empty;
                    break;
                case "Manufacturer":
                    _request.ProductId = string.Empty;
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
            _client.SubmitRemoval(_request.ProductId);
        }

        [Then(@"the result should be ok")]
        public void ThenTheResultShouldBeOk()
        {
            errorMessage.Should().BeEmpty();
        }

    }
}
