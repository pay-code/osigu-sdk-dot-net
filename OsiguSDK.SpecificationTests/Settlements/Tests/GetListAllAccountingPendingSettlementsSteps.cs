﻿using FluentAssertions;
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
    public class GetListAllAccountingPendingSettlementsSteps
    {
        private Pagination<SettlementAccountingPendingResponse> ListOfSettlements;
            
        [When(@"I request the get list of accounting pending settlements endpoint")]
        public void WhenIRequestTheGetListOfAccountingPendingSettlementsEndpoint()
        {
            try
            {
                ListOfSettlements =
                    Tools.RestClient.RequestToEndpoint<Pagination<SettlementAccountingPendingResponse>>(Method.GET,
                        "/settlements/account-pending");
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have the settlement client with an invalid slug")]
        public void GivenIHaveTheSettlementClientWithAnInvalidSlug()
        {
            Tools.RestClient = new RestClient(new Configuration
            {
                BaseUrl = Tools.ConfigSettlement.BaseUrl,
                Authentication = Tools.ConfigSettlement.Authentication,
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
