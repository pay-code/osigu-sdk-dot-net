using System;
using OsiguSDK.SpecificationTests.Tools;
using OsiguSDK.SpecificationTests.ResponseModels;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Accounting.Documents
{
    [Binding]
    public class CreateDocumentSteps
    {
        [Given(@"I have the create a new document request")]
        public void GivenIHaveTheCreateANewDocumentRequest()
        {
            Requests.DocumentRequest = TestClients.Fixture.Create<DocumentRequest>();
            Requests.DocumentRequest.Detail = null;
            Requests.DocumentRequest.Payments = null;
        }
        
        [Given(@"I have the create a new payment document request")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequest()
        {
            Requests.DocumentRequest = TestClients.Fixture.Create<DocumentRequest>();
        }
        
        [Given(@"I have the create a new payment document request with the same action type as the original")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithTheSameActionTypeAsTheOriginal()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have the create a new payment document request with an amount that doesnt match with the detail")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithAnAmountThatDoesntMatchWithTheDetail()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have the create a new payment document request with an amount that is higher than the original document")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithAnAmountThatIsHigherThanTheOriginalDocument()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have the create a new payment document request with an invalid amount")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithAnInvalidAmount()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have the create a new payment document request without the required fields")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithoutTheRequiredFields(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I request the create document endpoint")]
        public void WhenIRequestTheCreateDocumentEndpoint()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I request the create a payment document endpoint")]
        public void WhenIRequestTheCreateAPaymentDocumentEndpoint()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the document should be created successfully")]
        public void ThenTheDocumentShouldBeCreatedSuccessfully()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
