using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Tools;
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
                TestClients.InsurerAuthorizationClient.VoidAuthorization(Responses.AuthorizationId);
                errorMessage = new RequestException("ok", 204);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Then(@"the result should be forbidden for void the authorization")]
        public void ThenTheResultShouldBeForbiddenForVoidTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be not found for void the authorization")]
        public void ThenTheResultShouldBeNotFoundForVoidTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the result should be a valid response for void the authorization")]
        public void ThenTheResultShouldBeAValidResponseForVoidTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(204);
        }


    }
}
