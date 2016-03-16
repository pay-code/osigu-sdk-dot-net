using System;
using System.Linq;
using FluentAssertions;
using log4net.Util;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Insurers.Clients;
using TechTalk.SpecFlow;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Models;

namespace OsiguSDK.SpecificationTests.Products
{
    [Binding]
    public class InsurerGetListOfProductsSteps
    {
        private ProductsClient _insurerClient { get; set; }
        private Pagination<Product> listOfProducts { get; set; } 
        private string errorMessage { get; set; }

        [Given(@"I have the insurer products client whit an invalid token")]
        public void GivenIHaveTheInsurerProductsClientWhitAnInvalidToken()
        {
            _insurerClient = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigInsurer1Development.BaseUrl,
                Slug = Tools.ConfigInsurer1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the insurer products client with an invalid slug")]
        public void GivenIHaveTheInsurerProductsClientWithAnInvalidSlug()
        {
            _insurerClient = new ProductsClient(new Configuration
            {
                BaseUrl = Tools.ConfigInsurer1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigInsurer1Development.Authentication
            });
        }


        [Given(@"I have the insurer products client")]
        public void GivenIHaveTheInsurerProductsClient()
        {
            _insurerClient = new ProductsClient(Tools.ConfigInsurersSandbox);
        }

        [When(@"I make the get list of products request to the endpoint")]
        public void WhenIMakeTheGetListOfProductsRequestToTheEndpoint()
        {
            try
            {
               listOfProducts = _insurerClient.GetListOfProducts();
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        [Then(@"the result should be unauthorized for get a list of products")]
        public void ThenTheResultShouldBeUnauthorizedForGetAListOfProducts()
        {
            errorMessage.Should().Contain("Server failed to authenticate the request. Make sure the value of the Authorization header is formed correctly including the signature");
        }

        [Then(@"the result should be access denied for get a list of products")]
        public void ThenTheResultShouldBeAccessDeniedForGetAListOfProducts()
        {
            errorMessage.Should().Contain("You don’t have permission to access this resource");
        }

        [Then(@"the results should be list of products")]
        public void ThenTheResultsShouldBeListOfProducts()
        {
            if (listOfProducts.NumberOfElements > 0)
            {
                // the number of products is the same as defined in the response of the API
                listOfProducts.Content.Count.Should().Be(listOfProducts.TotalElements);
            }
            else
            {
                listOfProducts.Content.Count.Should().Be(0);
            }
        }

    }
}
