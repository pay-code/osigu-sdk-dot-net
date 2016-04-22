using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using RestSharp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    [Binding]
    public class GetListAllSettlementsSteps
    {
        Pagination<SettlementResponse> listOfSettlements;
        [Given(@"I have the settlement client")]
        public void GivenIHaveTheSettlementClient()
        {
            Tools.RestClient = new RestClient(ConfigurationClients.ConfigSettlement);
        }
        
        [When(@"I request the endpoint")]
        public void WhenIRequestTheEndpoint()
        {
            try
            {
                listOfSettlements = Tools.RestClient.RequestToEndpoint<Pagination<SettlementResponse>>(Method.GET,
                    "/settlements");
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }
        
        [Then(@"the settlement list should not be empty")]
        public void ThenTheSettlementListShouldNotBeEmpty()
        {
            listOfSettlements.Should().NotBeNull();
            listOfSettlements.Content.Should().NotBeNullOrEmpty();
            listOfSettlements.TotalElements.Should().Be(listOfSettlements.Content.Count);
        }

        [Given(@"I have the settlement client with an invalid token")]
        public void GivenIHaveTheSettlementClientWithAnInvalidToken()
        {
            Tools.RestClient = new RestClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigSettlement.BaseUrl,
                Authentication = new Authentication("NoAuth")
            });
        }
    }
}
