using System;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class GetAnExpressAuthorizationSteps
    {
        [When(@"I make the get express authorization request to the endpoint")]
        public void WhenIMakeTheGetExpressAuthorizationRequestToTheEndpoint()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should ok")]
        public void ThenTheResultShouldOk()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the authorization express data should be the expected")]
        public void ThenTheAuthorizationExpressDataShouldBeTheExpected()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
