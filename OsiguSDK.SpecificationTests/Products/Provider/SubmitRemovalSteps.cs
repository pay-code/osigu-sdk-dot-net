using System;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class SubmitARemovalSteps
    {
        private ProductsClient _client { get; set; }
        private SubmitProductRequest _request { get; set; }

        private string _productId { get; set; };
        private string errorMessage { get; set; }

        [Given(@"a product created")]
        public void GivenAProductCreated()
        {
            _request = Tools.Fixture.Create<SubmitProductRequest>();
            _request.ProductId = _request.ProductId.Substring(0, 25);
            _productId = _request.ProductId;
            _client.SubmitProduct(_request);
        }

        [When(@"I request the submit a removal endpoint")]
        public void WhenIRequestTheSubmitARemovalEndpoint()
        {
            try
            {
                _client.SubmitRemoval(_productId);
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }
    }
}
