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
                Tools.productsInsurerClient.SubmitProduct(Tools.submitInsurerProductRequest);
                errorMessage = string.Empty;
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

        [Then(@"the result should be unauthorized for adding a product")]
        public void ThenTheResultShouldBeUnauthorizedForAddingAProduct()
        {
            errorMessage.Should().Contain("Server failed to authenticate the request. Make sure the value of the Authorization header is formed correctly including the signature");
        }

        [Then(@"the result should be access denied for adding a product")]
        public void ThenTheResultShouldBeAccessDeniedForAddingAProduct()
        {
            errorMessage.Should().Contain("You don’t have permission to access this resource");
        }
        

        [Then(@"the response should be an error for adding a that product")]
        public void ThenTheResponseShouldBeAnErrorForAddingAThatProduct()
        {
            errorMessage.Should().Contain("id");
        }

        [Given(@"I have the request data for a new product whit an invalid drug type")]
        public void GivenIHaveTheRequestDataForANewProductWhitAnInvalidDrugType()
        {
            try
            {
                Tools.submitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Tools.submitInsurerProductRequest.Type = "1invalid_drug_type";
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        [Then(@"I have the request data for anew producto whit a repeated name")]
        public void ThenIHaveTheRequestDataForAnewProductoWhitARepeatedName()
        {
            try
            {
                SubmitProductRequest repeatedRequest = Tools.submitInsurerProductRequest;
                Tools.submitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Tools.submitInsurerProductRequest.Name = repeatedRequest.Name;
                Tools.submitInsurerProductRequest.FullName = repeatedRequest.FullName;
            }
            catch (Exception exception)
            {
                errorMessage = exception.Message;
            }
        }

        [Given(@"I have the request data for a new product whitout all the request parameters")]
        public void GivenIHaveTheRequestDataForANewProductWhitoutAllTheRequestParameters()
        {
            Tools.submitInsurerProductRequest.Name = String.Empty;
            Tools.submitInsurerProductRequest.FullName = String.Empty;

        }


        [Then(@"the response should be an error for adding that product")]
        public void ThenTheResponseShouldBeAnErrorForAddingThatProduct()
        {
            errorMessage.Should().Contain("error");
        }

        [Then(@"I have a (.*) response of adding that product")]
        public void ThenIHaveAResponseOfAddingThatProduct(int p0)
        {
            errorMessage.Should().BeEmpty();
        }





    }
}
