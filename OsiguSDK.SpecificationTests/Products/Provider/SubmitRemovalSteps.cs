using System;
using FluentAssertions;
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
        private string errorMessage { get; set; }

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
            Tools.ProductsProviderClient.SubmitProduct(Tools.SubmitProductRequest);
        }

        [When(@"I request the submit a removal endpoint")]
        public void WhenIRequestTheSubmitARemovalEndpoint()
        {
            try
            {
                Tools.ProductsProviderClient.SubmitRemoval(_productId);
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        [Then(@"the result should be product don't exists")]
        public void ThenTheResultShouldBeProductDonTExists()
        {
            errorMessage.Should().Contain("exist");
        }

        [Then(@"the result should be product status error")]
        public void ThenTheResultShouldBeProductStatusError()
        {
            errorMessage.Should().Contain("status");
        }

        [Then(@"the result should be product deleted successfully")]
        public void ThenTheResultShouldBeProductDeletedSuccessfully()
        {
            errorMessage.Should().BeEmpty();
        }
    }
}