using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class SubmitARemovalSteps
    {
        private string _productId { get; set; }

        [Given(@"I have the provider products client without the required permission")]
        public void GivenIHaveTheProviderProductsClientWithoutTheRequiredPermission()
        {
            Tools.ProductsProviderClient = new ProductsClient(Tools.ConfigProviderBranch1Development);
            Tools.ProductsProductsClientWithNoPermission = new ProductsClient(Tools.ConfigProviderBranch2Development);
        }


        [Given(@"a product created")]
        public void GivenAProductCreated()
        {
            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);
            _productId = Tools.SubmitProductRequest.ProductId;
            try
            {
                Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }
            
        }

        [When(@"I request the submit a removal endpoint")]
        public void WhenIRequestTheSubmitARemovalEndpoint()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitRemoval(_productId);
            }
            catch (RequestException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }
        }

        [Then(@"the result should be product don't exists")]
        public void ThenTheResultShouldBeProductDonTExists()
        {
            Tools.ErrorMessage.Should().Contain("exist");
        }

        [Then(@"the result should be product status error")]
        public void ThenTheResultShouldBeProductStatusError()
        {
            Tools.ErrorMessage.Should().Contain("status");
        }

        [Then(@"the result should be product deleted successfully")]
        public void ThenTheResultShouldBeProductDeletedSuccessfully()
        {
            Tools.ErrorMessage.Should().BeEmpty();
        }
    }
}