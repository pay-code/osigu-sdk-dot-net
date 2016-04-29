using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.ResponseModels;
using OsiguSDK.SpecificationTests.Tools;
using RestSharp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Accounting.DocumentTypes
{
    [Binding]
    public class GetOneDocumentTypeSteps
    {
        private static int _documentId = 2;
        [When(@"I request the get a document type endpoint")]
        public void WhenIRequestTheGetADocumentTypeEndpoint()
        {
            try
            {
                Responses.DocumentType = TestClients.InternalRestClient.RequestToEndpoint<DocumentType>(Method.GET,
                    string.Format("/document-types/{0}", _documentId));
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
            
        }
        
        [Then(@"the document type received should be the expected")]
        public void ThenTheDocumentTypeReceivedShouldBeTheExpected()
        {
            Responses.DocumentType.Id.Should().Be(_documentId);
            Responses.DocumentType.Name.Should().Be("settlement 2");
            Responses.DocumentType.Invoiceable.Should().BeFalse();
            Responses.DocumentType.MoneyFlow.Should().Be("in");
            Responses.DocumentType.Status.Should().Be("ACTIVE");
        }
    }
}
