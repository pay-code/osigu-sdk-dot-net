using System;
using FluentAssertions;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class GetProductSteps
    {
        private Product _responseProduct { get; set; }
        private string errorMessage { get; set; }

        [Given(@"I have an invalid product id")]
        public void GivenIHaveAnInvalidProductId()
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
        
        [When(@"I make the get a product request to the endpoint")]
        public void WhenIMakeTheGetAProductRequestToTheEndpoint()
        {
            try
            {
                _responseProduct = Tools.productsInsurerClient.GetSingleProduct(Tools.submitInsurerProductRequest.ProductId);
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
            
        }

        [Then(@"I have an error response of getting that product")]
        public void ThenIHaveAnErrorResponseOfGettingThatProduct()
        {
            errorMessage.Should().Contain("Product not found");
        }

        [Then(@"I have an ok response of adding that product")]
        public void ThenIHaveAnOkResponseOfAddingThatProduct()
        {
            errorMessage.Should().BeEmpty();
        }


        [Then(@"the result should be unauthorized for getting a product")]
        public void ThenTheResultShouldBeUnauthorizedForGettingAProduct()
        {
            errorMessage.Should().Contain("You don't have permission to access this resource");
        }

        [Then(@"the result should be access denied for getting a product")]
        public void ThenTheResultShouldBeAccessDeniedForGettingAProduct()
        {
            errorMessage.Should().Contain("Access denied");
        }

        [Then(@"a message error because the product does not exist")]
        public void ThenAMessageErrorBecauseTheProductDoesNotExist()
        {
            errorMessage.Should().Contain("Not Found");
        }
        

        [Then(@"the result should be the product information")]
        public void ThenTheResultShouldBeTheProductInformation()
        {
            _responseProduct.Should().NotBeNull();
            _responseProduct.Should().Be(new Product(), " Expected a prodct information");
            _responseProduct.Status.Should().Be("pending_review", "The product should not have been reviewed just yet");
            _responseProduct.Type.Should().Be(Tools.submitInsurerProductRequest.Type);
        }

    }
}
