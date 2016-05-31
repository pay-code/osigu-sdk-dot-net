using System;
using OsiguSDK.Core.Config;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;
using OsiguSDK.Core.Authentication;
using System.Configuration;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers
{
    [Binding]
    public class GetAnExpressAuthorizationAsAnInsurerSteps
    {
        [Given(@"I have the insurer express authorizations client with an invalid token")]
        public void GivenIHaveTheInsurerExpressAuthorizationsClientWithAnInvalidToken()
        {
            try
            {
                IConfiguration badConfiguration = ConfigurationClients.ConfigInsurer1;
                badConfiguration.Authentication = new Authentication(":(");
                TestClients.InternalRestClient = new InternalRestClient(ConfigurationClients.ConfigInsurer1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
        
        [Given(@"I have an invalid express authorization id")]
        public void GivenIHaveAnInvalidExpressAuthorizationId()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I make the get express authorization request to the endpoint as an insurer")]
        public void WhenIMakeTheGetExpressAuthorizationRequestToTheEndpointAsAnInsurer()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be forbidden for getting the express authorization")]
        public void ThenTheResultShouldBeForbiddenForGettingTheExpressAuthorization()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
