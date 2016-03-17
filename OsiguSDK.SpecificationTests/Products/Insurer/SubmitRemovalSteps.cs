using System;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class SubmitProductRemovalAsAnInsurerSteps
    {
        private string errorMessage { get; set; }

        [Given(@"I have an unregistered product information")]
        public void GivenIHaveAnUnregisteredProductInformation()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I make the remove a product request to the endpoint")]
        public void WhenIMakeTheRemoveAProductRequestToTheEndpoint()
        {
            try
            {
                Tools.productsInsurerClient.SubmitRemoval(Tools.submitInsurerProductRequest.ProductId);
            }
            catch (Exception exception)
            {
                        
            }
            
        }
        
        [Then(@"the result should be unauthorized for removing a product")]
        public void ThenTheResultShouldBeUnauthorizedForRemovingAProduct()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be access denied for removing a product")]
        public void ThenTheResultShouldBeAccessDeniedForRemovingAProduct()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the response should be (.*) with product not found")]
        public void ThenTheResponseShouldBeWithProductNotFound(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"i have a (.*) response of adding that product")]
        public void ThenIHaveAResponseOfAddingThatProduct(int p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
