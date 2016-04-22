using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class InsurerGetListOfProductsSteps
    {
        private Pagination<Product> listOfProducts { get; set; } 
        private RequestException errorMessage { get; set; }

        [Given(@"I have the insurer products client with an invalid token")]
        public void GivenIHaveTheInsurerProductsClientWithAnInvalidToken()
        {
            Tools.productsInsurerClient = new ProductsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1Development.BaseUrl,
                Slug = ConfigurationClients.ConfigInsurer1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }


        [Given(@"I have the insurer products client with an invalid slug")]
        public void GivenIHaveTheInsurerProductsClientWithAnInvalidSlug()
        {
            Tools.productsInsurerClient = new ProductsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigInsurer1Development.Authentication
            });
        }


        [Given(@"I have the insurer products client")]
        public void GivenIHaveTheInsurerProductsClient()
        {
            Tools.productsInsurerClient = new ProductsClient(ConfigurationClients.ConfigInsurer1Development);
        }

        [When(@"I make the get list of products request to the endpoint")]
        public void WhenIMakeTheGetListOfProductsRequestToTheEndpoint()
        {
            try
            {
               listOfProducts = Tools.productsInsurerClient.GetListOfProducts();
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Then(@"the result should be unauthorized for get a list of products")]
        public void ThenTheResultShouldBeUnauthorizedForGetAListOfProducts()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be access denied for get a list of products")]
        public void ThenTheResultShouldBeAccessDeniedForGetAListOfProducts()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the results should be list of products")]
        public void ThenTheResultsShouldBeListOfProducts()
        {
            if (listOfProducts.NumberOfElements > 0)
            {
                listOfProducts.TotalElements.Should().Be(listOfProducts.NumberOfElements);
            }
            
        }
    }
}
