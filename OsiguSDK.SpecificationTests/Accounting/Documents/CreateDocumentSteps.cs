using System;
using System.Collections.Generic;
using OsiguSDK.SpecificationTests.Tools;
using OsiguSDK.SpecificationTests.ResponseModels;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Accounting.Documents
{
    [Binding]
    public class CreateDocumentSteps
    {
        public int DocumentType { get; set; }

        private static void CreatePaymentDocument(string paymentType = "cash", bool sameAmount = true, decimal amount = 0m, string bankName = "", string reference = "")
        {
            Requests.DocumentRequest = TestClients.Fixture.Create<DocumentRequest>();
            Requests.DocumentRequest.Detail = new DocumentDetail
            {
                DocumentId = Responses.Document.Id,
                Amount = Responses.Document.Amount
            };

            Requests.DocumentRequest.Payments = new List<DocumentPayment>
            {
                new DocumentPayment
                {
                    Amount = sameAmount ? Responses.Document.Amount : amount,
                    BankName = bankName,
                    Reference = reference,
                    Type = paymentType
                }
            };
        }


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
            CreatePaymentDocument();
            DocumentType = 2;
        }

        [Given(@"I have the create a new payment document request with the same action type as the original")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithTheSameActionTypeAsTheOriginal()
        {
            CreatePaymentDocument();
            DocumentType = 1;
        }
        
        [Given(@"I have the create a new payment document request with an amount that doesnt match with the detail")]
        public void GivenIHaveTheCreateANewPaymentDocumentRequestWithAnAmountThatDoesntMatchWithTheDetail()
        {
            CreatePaymentDocument();
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
