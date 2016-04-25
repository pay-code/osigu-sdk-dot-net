using FluentAssertions;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using OsiguSDK.SpecificationTests.Tools;
using RestSharp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    [Binding]
    public class GetListAllAccountingPendingSettlementsSteps
    {
        private Pagination<SettlementAccountingPendingResponse> ListOfSettlements;
            
        [When(@"I request the get list of accounting pending settlements endpoint")]
        public void WhenIRequestTheGetListOfAccountingPendingSettlementsEndpoint()
        {
            try
            {
                ListOfSettlements =
                    TestClients.GenericRestClient.RequestToEndpoint<Pagination<SettlementAccountingPendingResponse>>(Method.GET,
                        "/settlements/account-pending");
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have the settlement client with an invalid slug")]
        public void GivenIHaveTheSettlementClientWithAnInvalidSlug()
        {
            TestClients.GenericRestClient = new GenericRestClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigSettlement.BaseUrl,
                Authentication = ConfigurationClients.ConfigSettlement.Authentication,
                Slug = "OtherSlug"
            });
        }


        [Then(@"the accounting pending list should not be empty")]
        public void ThenTheAccountingPendingListShouldNotBeEmpty()
        {
            ListOfSettlements.Should().NotBeNull();
            ListOfSettlements.Content.Should().NotBeNullOrEmpty();
            ListOfSettlements.Content.Count.Should().Be(ListOfSettlements.TotalElements);
        }
    }
}
