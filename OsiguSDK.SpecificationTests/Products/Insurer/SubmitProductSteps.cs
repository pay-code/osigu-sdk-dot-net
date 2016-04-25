using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Insurer
{
    [Binding]
    public class SubmitProductAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private Product productResponse { get; set; }
    
        [Given(@"I have the request data for a new insurer product")]
        public void GivenIHaveTheRequestDataForANewInsurerProduct()
        {
            try
            {
                Requests.SubmitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Requests.SubmitInsurerProductRequest.ProductId = Requests.SubmitInsurerProductRequest.ProductId.Substring(0,
                    25);
                Requests.SubmitInsurerProductRequest.Type = "DRUG";
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }



        [When(@"I make the add a product insurer request to the endpoint")]
        public void WhenIMakeTheAddAProductInsurerRequestToTheEndpoint()
        {
            try
            {
                Tools.ProductsInsurerClient.SubmitProduct(Requests.SubmitInsurerProductRequest);
                errorMessage = new RequestException("ok", 204);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
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
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be access denied for adding a product")]
        public void ThenTheResultShouldBeAccessDeniedForAddingAProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }
        

        [Then(@"the response should be an error for adding a that product")]
        public void ThenTheResponseShouldBeAnErrorForAddingAThatProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Given(@"I have the request data for a new product whit an invalid drug type")]
        public void GivenIHaveTheRequestDataForANewProductWhitAnInvalidDrugType()
        {
            try
            {
                Requests.SubmitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Requests.SubmitInsurerProductRequest.ProductId = Requests.SubmitInsurerProductRequest.ProductId.Substring(0,
                    25);
                Requests.SubmitInsurerProductRequest.Type = "1invalid_drug_type";
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }


        [Then(@"I have the request data for a new product whit a repeated name")]
        public void ThenIHaveTheRequestDataForANewProductWhitARepeatedName()
        {
            try
            {
                SubmitProductRequest repeatedRequest = Requests.SubmitInsurerProductRequest;
                Requests.SubmitInsurerProductRequest = Tools.Fixture.Create<SubmitProductRequest>();
                Requests.SubmitInsurerProductRequest.ProductId = Requests.SubmitInsurerProductRequest.ProductId.Substring(0,
                    25);
                Requests.SubmitInsurerProductRequest.Name = repeatedRequest.Name;
                Requests.SubmitInsurerProductRequest.FullName = repeatedRequest.FullName;
                Requests.SubmitInsurerProductRequest.Type = "DRUG";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.StackTrace);
            }
        }


        [Given(@"I have the request data for a new product whitout all the request parameters")]
        public void GivenIHaveTheRequestDataForANewProductWhitoutAllTheRequestParameters()
        {
            Requests.SubmitInsurerProductRequest.Name = String.Empty;
            Requests.SubmitInsurerProductRequest.FullName = String.Empty;

        }


        [Then(@"the response should be an error for adding that product")]
        public void ThenTheResponseShouldBeAnErrorForAddingThatProduct()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the response should be no content for adding a that product")]
        public void ThenTheResponseShouldBeNoContentForAddingAThatProduct()
        {
            errorMessage.ResponseCode.Should().Be(204);
        }

        [Then(@"the response should be unproccessable for adding that product")]
        public void ThenTheResponseShouldBeUnproccessableForAddingThatProduct()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }


        [Then(@"I have ok response of adding that product")]
        public void ThenIHaveOkResponseOfAddingThatProduct()
        {
            errorMessage.ResponseCode.Should().Be(204);
        }





    }
}
