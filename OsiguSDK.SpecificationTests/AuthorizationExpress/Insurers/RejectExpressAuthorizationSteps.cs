using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers
{
    [Binding]
    public class RejectAnExpressAuthorizationAsAnInsurerSteps
    {
        private RequestException _errorMessage;
        [When(@"I make the reject express authorization request to the endpoint as an insurer")]
        public void WhenIMakeTheRejectExpressAuthorizationRequestToTheEndpointAsAnInsurer()
        {
            try
            {
                TestClients.InsurerExpressAuthorizationClient.RejectExpressAuthorization(Responses.ExpressAuthorizationId);
                _errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                _errorMessage = exception;
            }
        }
        
        [Then(@"the result should be forbidden for rejecting the express authorization")]
        public void ThenTheResultShouldBeForbiddenForRejectingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(401);
        }

        [Then(@"the result should be not found for rejecting the express authorization")]
        public void ThenTheResultShouldBeNotFoundForRejectingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the result should be ok for rejecting the express authorization")]
        public void ThenTheResultShouldBeOkForRejectingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(200);
        }

        [Then(@"the result should be unprocesable for rejecting the express authorization")]
        public void ThenTheResultShouldBeUnprocesableForRejectingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(422);
        }

    }
}
