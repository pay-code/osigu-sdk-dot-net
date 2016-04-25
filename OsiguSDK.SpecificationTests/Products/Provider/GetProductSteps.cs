using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Provider
{
    [Binding]
    public class GettingAProductAsAnInsurerSteps
    {
        private Product Product { get; set; }
        [Given(@"I have the request data for a new product")]
        public void GivenIHaveTheRequestDataForANewProduct()
        {
            Requests.SubmitProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
            Requests.SubmitProductRequest.ProductId = Requests.SubmitProductRequest.ProductId.Substring(0, 25);
        }
        
        [When(@"I make the get a product provider request to the endpoint")]
        public void WhenIMakeTheGetAProductProviderRequestToTheEndpoint()
        {
            try
            {
                Product = TestClients.ProductsProviderClient.GetSingleProduct(Requests.SubmitProductRequest != null
                    ? Requests.SubmitProductRequest.ProductId
                    : "anyProduct");
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have an invalid provider product id")]
        public void GivenIHaveAnInvalidProviderProductId()
        {
            Requests.SubmitProductRequest = TestClients.Fixture.Create<SubmitProductRequest>();
        }

        [Then(@"the result should be the product does not exist")]
        public void ThenTheResultShouldBeTheProductDoesNotExist()
        {
            Responses.ErrorId.Should().Be(404);
        }

        [Then(@"the result should be the providers product information")]
        public void ThenTheResultShouldBeTheProvidersProductInformation()
        {
            Product.Should().NotBeNull();
            Product.ProductId.Should().Be(Requests.SubmitProductRequest.ProductId);
            Product.FullName.Should().Be(Requests.SubmitProductRequest.FullName);
            Product.Name.Should().Be(Requests.SubmitProductRequest.Name);
            //Product.Manufacturer.Should().Be(Requests.SubmitProductRequest.Manufacturer);
        }

    }
}
