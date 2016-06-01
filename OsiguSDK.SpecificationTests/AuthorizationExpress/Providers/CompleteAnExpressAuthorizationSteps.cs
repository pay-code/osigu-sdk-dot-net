using System;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class CompleteAnExpressAuthorizationSteps
    {
        [Given(@"I have the request data for complete an express authorization")]
        public void GivenIHaveTheRequestDataForCompleteAnExpressAuthorization()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have entered an invalid authorization id")]
        public void GivenIHaveEnteredAnInvalidAuthorizationId()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The amount of invoice is negative")]
        public void GivenTheAmountOfInvoiceIsNegative()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The amount of invoice is equal to cero")]
        public void GivenTheAmountOfInvoiceIsEqualToCero()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The amount of invoice is greater than sum of products")]
        public void GivenTheAmountOfInvoiceIsGreaterThanSumOfProducts()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The amount of invoice is less than sum of products")]
        public void GivenTheAmountOfInvoiceIsLessThanSumOfProducts()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"The required fields are missing")]
        public void GivenTheRequiredFieldsAreMissing()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I make the complete express authorization request to the endpoint")]
        public void WhenIMakeTheCompleteExpressAuthorizationRequestToTheEndpoint()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the result should be the express authorization including the invoice sent")]
        public void ThenTheResultShouldBeTheExpressAuthorizationIncludingTheInvoiceSent()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
