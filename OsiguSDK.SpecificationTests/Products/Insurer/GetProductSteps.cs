using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
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
                Requests.SubmitInsurerProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
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
                _responseProduct = TestClients.ProductsInsurerClient.GetSingleProduct(Requests.SubmitInsurerProductRequest.ProductId);
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
                .Be(Requests.SubmitInsurerProductRequest.ProductId, "The product should have the same id as the previos");
            _responseProduct.Name.Should()
                .Be(Requests.SubmitInsurerProductRequest.Name, "The product should have the same name as the previous one");
            _responseProduct.FullName.Should()
                .Be(Requests.SubmitInsurerProductRequest.FullName, "The product should have the same full name as the previous one");
            _responseProduct.Type.ToUpper().Should().Be(Requests.SubmitInsurerProductRequest.Type.ToUpper());
            _responseProduct.Status.Should().Be("pending_review", "The product should not have been reviewed just yet");
        }

    }
}
