using System;
using FluentAssertions;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class SubmitProductAsAnInsurerSteps
    {
        private string errorMessage { get; set; }
        private Product productResponse { get; set; }

        [Given(@"I have the request data for a new product")]
        public void GivenIHaveTheRequestDataForANewProduct()
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
        
        [When(@"I make the add a product insurer request to the endpoint")]
        public void WhenIMakeTheAddAProductInsurerRequestToTheEndpoint()
        {
            try
            {
                // La respuesta es correcta?
                productResponse = Tools.productsInsurerClient.SubmitProduct(Tools.submitInsurerProductRequest);
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        [Then(@"the response should be ok with code (.*)")]
        public void ThenTheResponseShouldBeOkWithCode(int p0)
        {
            productResponse.ProductId.ShouldBeEquivalentTo(p0);
        }

    }
}
