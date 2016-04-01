using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using TechTalk.SpecFlow;
using OsiguSDK.Insurers.Models;

namespace OsiguSDK.SpecificationTests.Authorizations.Insurers
{
    [Binding]
    public class UpdateAnAuthorizationAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private Authorization responseAuthorization { get; set; }
        [When(@"I make the update authorization request to the endpoint")]
        public void WhenIMakeTheUpdateAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.insurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId,Tools.submitAuthorizationRequest);
                Tools.AuthorizationId = responseAuthorization.Id;
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }
        
        [Then(@"the result should be forbidden for updating the authorization")]
        public void ThenTheResultShouldBeForbiddenForUpdatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be not found for updating the authorization")]
        public void ThenTheResultShouldBeNotFoundForUpdatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

    }
}
