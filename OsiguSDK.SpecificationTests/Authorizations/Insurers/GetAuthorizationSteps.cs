using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models;
using OsiguSDK.SpecificationTests.Tools;
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
                responseAuthorization = TestClients.InsurerAuthorizationClient.GetSingleAuthorization(Responses.AuthorizationId);
                Responses.AuthorizationId = responseAuthorization.Id;
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
            Responses.AuthorizationId = "12345";
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
                responseAuthorization = TestClients.InsurerAuthorizationClient.GetSingleAuthorization(Responses.AuthorizationId);
                Responses.AuthorizationId = responseAuthorization.Id;
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
            responseAuthorization.AuthorizationDate.Date.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Requests.SubmitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Requests.SubmitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Requests.SubmitAuthorizationRequest.Items.Count);
        }

    }
}
