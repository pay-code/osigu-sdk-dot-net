using System;
using FluentAssertions;
using OsiguSDK.Insurers.Models.Requests;
using Ploeh.AutoFixture;
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
            try
            {
                Tools.submitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
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
                errorMessage = exception.Message;
            }
            
        }
        
        [Then(@"the result should be unauthorized for removing a product")]
        public void ThenTheResultShouldBeUnauthorizedForRemovingAProduct()
        {
            errorMessage.Should().Contain("Server failed to authenticate the request. Make sure the value of the Authorization header is formed correctly including the signature");
        }
        
        [Then(@"the result should be access denied for removing a product")]
        public void ThenTheResultShouldBeAccessDeniedForRemovingAProduct()
        {
            errorMessage.Should().Contain("You don’t have permission to access this resource");
        }
        
        [Then(@"the response should be (.*) with product not found")]
        public void ThenTheResponseShouldBeWithProductNotFound(int p0)
        {
            errorMessage?.Should().Contain("exist");
        }

        [Then(@"the response should be ok for removing the product")]
        public void ThenTheResponseShouldBeOkForRemovingTheProduct()
        {
            errorMessage?.Should().BeEmpty();
        }

        [Then(@"the response should be an error for removing the product")]
        public void ThenTheResponseShouldBeAnErrorForRemovingTheProduct()
        {
            errorMessage?.Should().Contain("found");
        }

    }
}
