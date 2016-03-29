using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
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
            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
            Tools.SubmitProductRequest.ProductId = Tools.SubmitProductRequest.ProductId.Substring(0, 25);
        }
        
        [When(@"I make the get a product provider request to the endpoint")]
        public void WhenIMakeTheGetAProductProviderRequestToTheEndpoint()
        {
            try
            {
                Product = Tools.ProductsProviderClient.GetSingleProduct(Tools.SubmitProductRequest != null
                    ? Tools.SubmitProductRequest.ProductId
                    : "anyProduct");
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have an invalid provider product id")]
        public void GivenIHaveAnInvalidProviderProductId()
        {
            Tools.SubmitProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
        }

        [Then(@"the result should be the product does not exist")]
        public void ThenTheResultShouldBeTheProductDoesNotExist()
        {
            Tools.ErrorId.Should().Be(404);
        }

        [Then(@"the result should be the providers product information")]
        public void ThenTheResultShouldBeTheProvidersProductInformation()
        {
            Product.Should().NotBeNull();
            Product.ProductId.Should().Be(Tools.SubmitProductRequest.ProductId);
            Product.FullName.Should().Be(Tools.SubmitProductRequest.FullName);
            Product.Name.Should().Be(Tools.SubmitProductRequest.Name);
            //Product.Manufacturer.Should().Be(Tools.SubmitProductRequest.Manufacturer);
        }

    }
}
