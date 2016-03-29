using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class GettingAProductAsAnInsurerSteps
    {
        [Given(@"I have the request data for a new product")]
        public void GivenIHaveTheRequestDataForANewProduct()
        {
            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);
        }
        
        [When(@"I make the get a product provider request to the endpoint")]
        public void WhenIMakeTheGetAProductProviderRequestToTheEndpoint()
        {
            try
            {
                Tools.ProductsProviderClient.GetSingleProduct(Tools.SubmitProductRequest != null
                    ? Tools.SubmitProductRequest.ProductId
                    : "anyProduct");
            }
            catch (RequestException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }
        }
    }
}
