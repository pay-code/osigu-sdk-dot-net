using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers
{
    [Binding]
    public class ApproveAnExpressAuthorizationAsAnInsurerSteps
    {
        private RequestException _errorMessage;
        [When(@"I make the approve express authorization request to the endpoint as an insurer")]
        public void WhenIMakeTheApproveExpressAuthorizationRequestToTheEndpointAsAnInsurer()
        {
            try
            {
                TestClients.InsurerExpressAuthorizationClient.ApproveExpressAuthorization(Responses.ExpressAuthorizationId);
                _errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                _errorMessage = exception;
            }
        }
        
        [Then(@"the result should be forbidden for approving the express authorization")]
        public void ThenTheResultShouldBeForbiddenForApprovingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(401);
        }


        [Then(@"the result should be not found for approving the express authorization")]
        public void ThenTheResultShouldBeNotFoundForApprovingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the result should be ok for approving the express authorization")]
        public void ThenTheResultShouldBeOkForApprovingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(200);
        }

        [Then(@"I have the insurer express authorizations client")]
        public void ThenIHaveTheInsurerExpressAuthorizationsClient()
        {
            try
            {
                TestClients.InsurerExpressAuthorizationClient = new ExpressAuthorizationClient(ConfigurationClients.ConfigInsurer1);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"the result should be not procesable for approving the express authorization")]
        public void ThenTheResultShouldBeNotProcesableForApprovingTheExpressAuthorization()
        {
            _errorMessage.ResponseCode.Should().Be(422);
        }


    }
}
