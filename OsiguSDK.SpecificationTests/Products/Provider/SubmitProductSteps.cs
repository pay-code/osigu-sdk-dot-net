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
            catch (ServiceException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }

        }

        [When(@"I request the submit a removal endpoint without permission")]
        public void WhenIRequestTheSubmitARemovalEndpointWithoutPermission()
        {
            try
            {
                Tools.ProductsProductsClientWithNoPermission.SubmitProduct(Tools.SubmitProductRequest);
            }
            catch (ServiceException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }
        }


        [Then(@"the result should be unauthorized")]
        public void ThenTheResultShouldBeUnauthorized()
        {
            Tools.ErrorMessage.Should().Be("You don't have permission to access this resource");
        }

        [Then(@"the result should be no permission")]
        public void ThenTheResultShouldBeNoPermission()
        {
            Tools.ErrorMessage.Should().Contain("Access is denied");
        }


        [When(@"I request the submit a product endpoint twice")]
        public void WhenIRequestTheSubmitAProductEndpointTwice()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
                Tools.ErrorMessage = string.Empty;
            }
            catch (ServiceException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                newRequest.ProductId = Tools.SubmitProductRequest.ProductId;
                Tools.ProductsProviderClient.SubmitProduct(newRequest);
            }
            catch (ServiceException exception)
            {
                Tools.ErrorMessage2 = exception.Message;
            }
        }

        [Then(@"the result should be ok on the first")]
        public void ThenTheResultShouldBeOkOnTheFirst()
        {
            Tools.ErrorMessage.Should().BeEmpty();
        }

        [Then(@"the result should be error on the second")]
        public void ThenTheResultShouldBeErrorOnTheSecond()
        {
            Tools.ErrorMessage2.Should().Contain("id");
        }

        [When(@"I request the submit a product endpoint twice with the same name")]
        public void WhenIRequestTheSubmitAProductEndpointTwiceWithTheSameName()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
                Tools.ErrorMessage = string.Empty;
            }
            catch (ServiceException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }

            try
            {
                var newRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);
                newRequest.Name = Tools.SubmitProductRequest.Name;
                Tools.ProductsProviderClient.SubmitProduct(newRequest);
            }
            catch (ServiceException exception)
            {
                Tools.ErrorMessage2 = exception.Message;
            }
        }


        [Then(@"the result should be error because of repeated name on the second")]
        public void ThenTheResultShouldBeErrorBecauseOfRepeatedNameOnTheSecond()
        {
            Tools.ErrorMessage2.Should().Contain("name");
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
            Tools.ErrorMessage.Should().Contain("missing");
        }

        [When(@"the product is removed")]
        public void WhenTheProductIsRemoved()
        {
            Tools.ProductsProviderClient.SubmitRemoval(Tools.SubmitProductRequest.ProductId);
        }

        [Then(@"the result should be ok")]
        public void ThenTheResultShouldBeOk()
        {
            Tools.ErrorMessage.Should().BeEmpty();
        }

    }
}