using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Models;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class GetListProductsSteps
    {

        private Pagination<Product> ListOfProducts { get; set; }
        [When(@"I make the get list of provider products request to the endpoint")]
        public void WhenIMakeTheGetListOfProviderProductsRequestToTheEndpoint()
        {
            try
            {
                ListOfProducts = Tools.ProductsProviderClient.GetListOfProducts();
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
            
        }

        [Then(@"the results should be list of provider products")]
        public void ThenTheResultsShouldBeListOfProviderProducts()
        {
            ListOfProducts.Should().NotBeNull();
            ListOfProducts.Content.Should().NotBeNull();
            ListOfProducts.Content.Should().NotBeEmpty();
            ListOfProducts.TotalElements.Should().Be(ListOfProducts.Content.Count);
        }

    }
}
