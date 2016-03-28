using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using TechTalk.SpecFlow;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Models;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class InsurerGetListOfProductsSteps
    {
        private Pagination<Product> listOfProducts { get; set; } 
        private string errorMessage { get; set; }

        [Given(@"I have the insurer products client with an invalid token")]
        public void GivenIHaveTheInsurerProductsClientWithAnInvalidToken()
        {
            Tools.productsInsurerClient = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigInsurer1Development.BaseUrl,
                Slug = Tools.ConfigInsurer1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }


        [Given(@"I have the insurer products client with an invalid slug")]
        public void GivenIHaveTheInsurerProductsClientWithAnInvalidSlug()
        {
            Tools.productsInsurerClient = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigInsurer1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigInsurer1Development.Authentication
            });
        }


        [Given(@"I have the insurer products client")]
        public void GivenIHaveTheInsurerProductsClient()
        {
            Tools.productsInsurerClient = new ProductsClient(Tools.ConfigInsurer1Development);
        }

        [When(@"I make the get list of products request to the endpoint")]
        public void WhenIMakeTheGetListOfProductsRequestToTheEndpoint()
        {
            try
            {
               listOfProducts = Tools.productsInsurerClient.GetListOfProducts();
            }
            catch (ServiceException exception)
            {
                errorMessage = exception.Message;
            }
        }

        [Then(@"the result should be unauthorized for get a list of products")]
        public void ThenTheResultShouldBeUnauthorizedForGetAListOfProducts()
        {
            errorMessage.Should().Contain("You don't have permission to access this resource");
        }

        [Then(@"the result should be access denied for get a list of products")]
        public void ThenTheResultShouldBeAccessDeniedForGetAListOfProducts()
        {
            errorMessage.Should().Contain("Access denied");
        }

        [Then(@"the results should be list of products")]
        public void ThenTheResultsShouldBeListOfProducts()
        {
            listOfProducts.Content.Count.Should()
                .Be(listOfProducts.NumberOfElements > 0 ? listOfProducts.TotalElements : 0);
        }
    }
}
