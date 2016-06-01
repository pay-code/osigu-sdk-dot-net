using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers
{
    [Binding]
    public class GetListOfExpressAuthorizationsAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private Pagination<ExpressAuthorization> responseAuthorizations;
        [When(@"I make the get list of express authorizations request to the endpoint as an insurer")]
        public void WhenIMakeTheGetListOfExpressAuthorizationsRequestToTheEndpointAsAnInsurer()
        {
            try
            {
                responseAuthorizations =
                    TestClients.InsurerExpressAuthorizationClient.GetListOfAuthorizationsExpress(Requests.expressAuthorizationStatus);
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
}
        
        [Then(@"the result should be forbidden for getting the list of express authorizations as an insurer")]
        public void ThenTheResultShouldBeForbiddenForGettingTheListOfExpressAuthorizationsAsAnInsurer()
        {
            errorMessage.ResponseCode.Should().Be(401);
        }

        [Then(@"the result should be not found for getting the list of express authorizations as an insurer")]
        public void ThenTheResultShouldBeNotFoundForGettingTheListOfExpressAuthorizationsAsAnInsurer()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Given(@"I have an invalid authorization status")]
        public void GivenIHaveAnInvalidAuthorizationStatus()
        {
            ScenarioContext.Current.Pending();
            
        }

        [Given(@"I request the authorizations status '(.*)' and id (.*)")]
        public void GivenIRequestTheAuthorizationsStatusAndId(string p0, int p1)
        {
            Requests.expressAuthorizationStatus = GetAuthorizationStatus(p1);
        }


        [Then(@"the result should be the list of express authorizations")]
        public void ThenTheResultShouldBeTheListOfExpressAuthorizations()
        {
            errorMessage.ResponseCode.Should().Be(200);
            responseAuthorizations.NumberOfElements.Equals(responseAuthorizations.Content.Count);
            //TODO
            // incluir si es necesario las pruebas para los elementos devueltos
            //foreach (var respuesta in responseAuthorizations.Content)
            //{
            //  //  respuesta.
            //}
        }


        public ExpressAuthorizationClient.ExpressAuthorizationStatus GetAuthorizationStatus(int type)
        {
            switch (type)
            {
                case 1: return ExpressAuthorizationClient.ExpressAuthorizationStatus.INSURER_PENDING_REVIEW;
                default: return ExpressAuthorizationClient.ExpressAuthorizationStatus.PAID;
            }
        }
    }
}
