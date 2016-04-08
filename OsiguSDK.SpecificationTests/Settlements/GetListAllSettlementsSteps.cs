using System;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements
{
    [Binding]
    public class GetListAllSettlementsSteps
    {
        [Given(@"I have the settlement client")]
        public void GivenIHaveTheSettlementClient()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I request the endpoint")]
        public void WhenIRequestTheEndpoint()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the settlement list should not be empty")]
        public void ThenTheSettlementListShouldNotBeEmpty()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
