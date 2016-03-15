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
        //private readonly Fixture _fixture = new Fixture();
        [Given(@"I have the provider products client")]
        public void GivenIHaveTheProviderProductsClient()
        {
            _client = new ProductsClient(Tools.ConfigProvidersSandbox);
        }
        
        [Given(@"the submit a product request")]
        public void GivenTheSubmitAProductRequest()
        {
            //_request = _fixture.Create<SubmitProductRequest>();
            _request = new SubmitProductRequest
            {
                FullName = "Dexlansoprazole 30 capsules of Dexilant 60mg",
                Manufacturer = "Takeda Pharmaceuticals",
                Name = "Dexlansoprazole",
                ProductId = "M215"
            };
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
