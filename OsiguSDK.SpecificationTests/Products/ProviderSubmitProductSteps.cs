using TechTalk.SpecFlow;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;


namespace OsiguSDK.SpecificationTests.Products
{
    [Binding]
    public class ProviderSubmitProductSteps
    {
        private ProductsClient _client { get; set; }
        private SubmitProductRequest _request { get; set; }
        [Given(@"I have the provider products client")]
        public void GivenIHaveTheProviderProductsClient()
        {
            _client = new ProductsClient(Tools.ConfigProvidersSandbox);
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
            _client.SubmitProduct(_request);
        }
        
        [Then(@"the result should be unauthorized")]
        public void ThenTheResultShouldBeUnauthorized()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
