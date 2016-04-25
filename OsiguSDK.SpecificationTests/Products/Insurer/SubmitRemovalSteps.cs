using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class SubmitProductRemovalAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }

        [Given(@"I have an unregistered product information")]
        public void GivenIHaveAnUnregisteredProductInformation()
        {
            try
            {
                Requests.SubmitInsurerProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }
        
        [When(@"I make the remove a product request to the endpoint")]
        public void WhenIMakeTheRemoveAProductRequestToTheEndpoint()
        {
            try
            {
                TestClients.ProductsInsurerClient.SubmitRemoval(Requests.SubmitInsurerProductRequest.ProductId);
                errorMessage = new RequestException("ok", 204);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }

        }
        
        [Then(@"the result should be unauthorized for removing a product")]
        public void ThenTheResultShouldBeUnauthorizedForRemovingAProduct()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }
        
        [Then(@"the result should be access denied for removing a product")]
        public void ThenTheResultShouldBeAccessDeniedForRemovingAProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the response should be an error for product not found")]
        public void ThenTheResponseShouldBeAnErrorForProductNotFound()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }
        
        [Then(@"the response should be ok for removing the product")]
        public void ThenTheResponseShouldBeOkForRemovingTheProduct()
        {
            errorMessage.ResponseCode.Should().Be(204);
        }

        [Then(@"the response should be an error for removing the product")]
        public void ThenTheResponseShouldBeAnErrorForRemovingTheProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

    }
}
