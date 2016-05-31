using System;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class CreateExpressAuthorizationSteps
    {
        [Given(@"I have entered a valid policy holder")]
        public void GivenIHaveEnteredAValidPolicyHolder()
        {
            
        }
        
        [Given(@"I have the request data for a new express authorization")]
        public void GivenIHaveTheRequestDataForANewExpressAuthorization()
        {
            
        }
        
        [When(@"I make the new express authorization request to the endpoint")]
        public void WhenIMakeTheNewExpressAuthorizationRequestToTheEndpoint()
        {
            
        }
        
        [Then(@"the result should be Accepted")]
        public void ThenTheResultShouldBeAccepted()
        {
            
        }
        
        [Then(@"the headers should contains the queue id")]
        public void ThenTheHeadersShouldContainsTheQueueId()
        {
            
        }

        [Given(@"I have entered an invalid policy holder '(.*)'")]
        public void GivenIHaveEnteredAnInvalidPolicyHolder(string policyHolderField)
        {
            
        }

        [Given(@"I have not included a insurer")]
        public void GivenIHaveNotIncludedAInsurer()
        {
            
        }

        [Given(@"I have not included a policy holder")]
        public void GivenIHaveNotIncludedAPolicyHolder()
        {
            
        }

        [Given(@"I have entered an invalid provider slug")]
        public void GivenIHaveEnteredAnInvalidProviderSlug()
        {
            
        }



    }
}
