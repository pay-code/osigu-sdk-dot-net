using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Tools;
using OsiguSDK.SpecificationTests.ResponseModels;
using RestSharp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Accounting.DocumentTypes
{
    [Binding]
    public class ListAllDocumentTypesSteps
    {
        [Given(@"I have the accounting client")]
        public void GivenIHaveTheAccountingClient()
        {
            TestClients.InternalRestClient = new InternalRestClient(ConfigurationClients.ConfigInternal);
        }
        
        [When(@"I request the get all document types endpoint")]
        public void WhenIRequestTheGetAllDocumentTypesEndpoint()
        {
            try
            {
                Responses.DocumentTypes = TestClients.InternalRestClient.RequestToEndpoint<Pagination<DocumentType>>(Method.GET, "/document-types");
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
            
        }
        
        [Then(@"the amount of document types should be the expected")]
        public void ThenTheAmountOfDocumentTypesShouldBeTheExpected()
        {
            Responses.DocumentTypes.Content.Count.Should().BeGreaterThan(1);
        }
    }
}
