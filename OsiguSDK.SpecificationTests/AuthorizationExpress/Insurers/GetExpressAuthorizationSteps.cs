using System;
using OsiguSDK.Core.Config;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;
using OsiguSDK.Core.Authentication;
using System.Configuration;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using RestSharp;
using Configuration = OsiguSDK.Core.Config.Configuration;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers
{
    [Binding]
    public class GetAnExpressAuthorizationAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private OsiguSDK.Insurers.Models.ExpressAuthorization responsExpressAuthorization { get; set; }
        [Given(@"I have the insurer express authorizations client with an invalid token")]
        public void GivenIHaveTheInsurerExpressAuthorizationsClientWithAnInvalidToken()
        {

            try
            {
                TestClients.InsurerExpressAuthorizationClient = new ExpressAuthorizationClient(new Configuration
                {
                    BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                    Slug = ConfigurationClients.ConfigInsurer1.Slug,
                    Authentication = new Authentication("noOAuthToken :(")
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        [Given(@"I have the insurer express authorizations client with an invalid slug")]
        public void GivenIHaveTheInsurerExpressAuthorizationsClientWithAnInvalidSlug()
        {
            try
            {
                TestClients.InsurerExpressAuthorizationClient = new ExpressAuthorizationClient(new Configuration
                {
                    BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                    Slug = "another_slug",
                    Authentication = ConfigurationClients.ConfigInsurer1.Authentication
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        [When(@"I make the get express authorization request to the endpoint as an insurer")]
        public void WhenIMakeTheGetExpressAuthorizationRequestToTheEndpointAsAnInsurer()
        {
            try
            {
                responsExpressAuthorization =
                TestClients.InsurerExpressAuthorizationClient.GetSingleAuthorization(Responses.ExpressAuthorizationId);
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }
        
        [Then(@"the result should be forbidden for getting the express authorization")]
        public void ThenTheResultShouldBeForbiddenForGettingTheExpressAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(401);
        }

        [Then(@"the result should be not found for getting the express authorization")]
        public void ThenTheResultShouldBeNotFoundForGettingTheExpressAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Given(@"I have the insurer express authorizations client")]
        public void GivenIHaveTheInsurerExpressAuthorizationsClient()
        {
            try
            {
                TestClients.InsurerExpressAuthorizationClient = new ExpressAuthorizationClient(ConfigurationClients.ConfigInsurer1);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Given(@"I have an invalid express authorization id")]
        public void GivenIHaveAnInvalidExpressAuthorizationId()
        {
            Responses.ExpressAuthorizationId = "EXP-GT-123545";
        }

        [Given(@"I have a valid express authorization id")]
        public void GivenIHaveAValidExpressAuthorizationId()
        {
            Responses.ExpressAuthorizationId = "EXP-GT-YEOITD6";
        }

        [Then(@"I have a valid response for getting the express authorization as an insurer")]
        public void ThenIHaveAValidResponseForGettingTheExpressAuthorizationAsAnInsurer()
        {
            errorMessage.ResponseCode.Should().Be(200);
            // TODO
            // Asercion de los campos de respuesta
        }

    }
}
