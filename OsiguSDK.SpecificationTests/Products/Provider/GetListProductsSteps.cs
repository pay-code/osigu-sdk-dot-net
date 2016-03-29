using OsiguSDK.Core.Exceptions;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class GetListProductsSteps
    {
        [When(@"I make the get list of provider products request to the endpoint")]
        public void WhenIMakeTheGetListOfProviderProductsRequestToTheEndpoint()
        {
            try
            {
                Tools.ProductsProviderClient.GetListOfProducts();
            }
            catch (RequestException exception)
            {
                Tools.ErrorMessage = exception.Message;
            }
            
        }
    }
}
