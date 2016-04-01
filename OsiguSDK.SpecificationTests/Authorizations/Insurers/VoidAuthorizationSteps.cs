using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Authorizations.Insurers
{
    [Binding]
    public class VoidAnAuthorizationAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }

        [When(@"I make the void authorization request to the endpoint")]
        public void WhenIMakeTheVoidAuthorizationRequestToTheEndpoint()
        {
            try
            {
                Tools.insurerAuthorizationClient.VoidAuthorization(Tools.AuthorizationId);
                errorMessage = new RequestException("ok", 204);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }
        
        [Then(@"the result should be forbidden for voiding the authorization")]
        public void ThenTheResultShouldBeForbiddenForVoidingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }
    }
}
