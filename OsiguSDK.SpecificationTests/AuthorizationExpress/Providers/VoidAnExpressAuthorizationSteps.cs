using System;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class VoidAnExpressAuthorizationSteps
    {
        [Given(@"The authorization status is different to pending review or null")]
        public void GivenTheAuthorizationStatusIsDifferentToPendingReviewOrNull()
        {
            
        }


        [Given(@"I have entered a invalid authorization id")]
        public void GivenIHaveEnteredAInvalidAuthorizationId()
        {
            
        }
        
        [When(@"I make the void express authorization request to the endpoint")]
        public void WhenIMakeTheVoidExpressAuthorizationRequestToTheEndpoint()
        {
            
        }
        
        [Then(@"the result should be no content")]
        public void ThenTheResultShouldBeNoContent()
        {
            
        }
    }
}
