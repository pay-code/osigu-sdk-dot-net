using FluentAssertions;
using OsiguSDK.Core.Exceptions;
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
        private RequestException errorMessage { get; set; }

        [Given(@"I have an invalid product id")]
        public void GivenIHaveAnInvalidProductId()
        {
            try
            {
                Tools.submitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }
        
        [When(@"I make the get a product request to the endpoint")]
        public void WhenIMakeTheGetAProductRequestToTheEndpoint()
        {
            try
            {
                _responseProduct = Tools.productsInsurerClient.GetSingleProduct(Tools.submitInsurerProductRequest.ProductId);
                errorMessage = new RequestException("ok", 204);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
            
        }

        [Then(@"I have an error response of getting that product")]
        public void ThenIHaveAnErrorResponseOfGettingThatProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the result should be unauthorized for getting a product")]
        public void ThenTheResultShouldBeUnauthorizedForGettingAProduct()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be access denied for getting a product")]
        public void ThenTheResultShouldBeAccessDeniedForGettingAProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"a message error because the product does not exist")]
        public void ThenAMessageErrorBecauseTheProductDoesNotExist()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }
        

        [Then(@"the result should be the product information")]
        public void ThenTheResultShouldBeTheProductInformation()
        {
            errorMessage.ResponseCode.Should().Be(204);
            _responseProduct.Should().NotBeNull();
            _responseProduct.ProductId.Should()
                .Be(Tools.submitInsurerProductRequest.ProductId, "The product should have the same id as the previos");
            _responseProduct.Name.Should()
                .Be(Tools.submitInsurerProductRequest.Name, "The product should have the same name as the previous one");
            _responseProduct.FullName.Should()
                .Be(Tools.submitInsurerProductRequest.FullName, "The product should have the same full name as the previous one");
            _responseProduct.Type.ToUpper().Should().Be(Tools.submitInsurerProductRequest.Type.ToUpper());
            _responseProduct.Status.Should().Be("pending_review", "The product should not have been reviewed just yet");
        }

    }
}
