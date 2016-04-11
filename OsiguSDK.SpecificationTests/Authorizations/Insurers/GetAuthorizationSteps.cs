using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Authorizations.Insurers
{
    [Binding]
    public class GetAnAuthorizationAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private Authorization responseAuthorization { get; set; }

        [When(@"I make the get authorization request to the endpoint")]
        public void WhenIMakeTheGetAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.insurerAuthorizationClient.GetSingleAuthorization(Tools.AuthorizationId);
                Tools.AuthorizationId = responseAuthorization.Id;
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Given(@"I have an invalid authorization id")]
        public void GivenIHaveAnInvalidAuthorizationId()
        {
            Tools.AuthorizationId = "12345";
        }


        [Then(@"the result should be forbidden for getting the authorization")]
        public void ThenTheResultShouldBeForbiddenForGettingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be not found for getting the authorization")]
        public void ThenTheResultShouldBeNotFoundForGettingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"I make the get authorization request to the endpoint")]
        public void ThenIMakeTheGetAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.insurerAuthorizationClient.GetSingleAuthorization(Tools.AuthorizationId);
                Tools.AuthorizationId = responseAuthorization.Id;
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Then(@"I have a valid response for getting the authorization")]
        public void ThenIHaveAValidResponseForGettingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(200);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Tools.submitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Tools.submitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Tools.submitAuthorizationRequest.Items.Count);
        }

    }
}
