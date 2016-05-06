using System;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using OsiguSDK.SpecificationTests.Tools;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements.Create
{
    [Binding]
    public class GetSettlementsSteps
    {
        [Given(@"I created one '(.*)' settlment with (.*) claims with amount '(.*)'")]
        public void GivenICreatedOneSettlmentWithClaimsWithAmount(string settlementType, int numberOfClaimsToCreate, string claimAmountRange)
        {
            Requests.SettlementType = Utils.ParseEnum<SettlementType>(settlementType);
            var claimsAmountRange = Utils.ParseEnum<ClaimAmountRange>(claimAmountRange);

            Responses.Settlement = SettlementTool.CreateSettlement(Requests.SettlementType, numberOfClaimsToCreate, claimsAmountRange);
        }

        [When(@"I make the request to the list all settlements endpoint")]
        public void WhenIMakeTheRequestToTheListAllSettlementsEndpoint()
        {
            var page = "0";
            var size = "100";
            var pagination = $"?page={page}&size={size}";
            var response = TestClients.InternalRestClient.RequestToEndpoint<Pagination<SettlementResponse>>(Method.GET, pagination);

            if (!response.LastPage)
            {
                page = HttpUtility.ParseQueryString(response.Links.Last.Href).Get("page");
                size = HttpUtility.ParseQueryString(response.Links.Last.Href).Get("size");
                pagination = $"?page={page}&size={size}";
                response = TestClients.InternalRestClient.RequestToEndpoint<Pagination<SettlementResponse>>(Method.GET, pagination);
            }

            Responses.Settlements = response;
        }

        [Then(@"The result should be the list of all settlments")]
        public void ThenTheResultShouldBeTheListOfAllSettlments()
        {
            Responses.Settlements.Should().NotBeNull();
            Responses.Settlements.Content.Count.Should().BeGreaterThan(0);
        }

        [Then(@"The settlement created should be the expected")]
        public void ThenTheSettlementCreatedShouldBeTheExpected()
        {
            var settlement = Responses.Settlements.Content.First(x => x.Id == Responses.Settlement.Id);

            settlement.Status.Should().Be(SettlementStatus.CREATED.ToString());
            settlement.Type.Should().Be(Requests.SettlementType.ToString().ToUpper());
            settlement.DateFrom.Should().BeCloseTo(Requests.SettlementRequest.From,999);
            settlement.DateTo.Should().BeCloseTo(Requests.SettlementRequest.To,999);
            settlement.CurrencyCode.Should().Be("Q");
            settlement.InsurerId.Should().Be(int.Parse(Requests.SettlementRequest.InsurerId));
            settlement.ProviderId.Should().Be(int.Parse(Requests.SettlementRequest.ProviderId));
            settlement.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 500000);

            ValidateSettlementItems(settlement);
        }

        private static void ValidateSettlementItems(SettlementResponse settlement)
        {
            var claimsSent = CurrentData.Claims.Select(x => new
            {
                x.Id,
                x.Invoice.Amount,
            });

            var claimsExpected = settlement.Items.Select(x => new
            {
                Id = x.ClaimId,
                Amount = x.ClaimAmount
            });

            claimsSent.ShouldAllBeEquivalentTo(claimsExpected);

            foreach (var item in Responses.Settlement.Items)
            {
                item.Id.Should().BeGreaterThan(0);
                item.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 30000);
            }

        }
    }
}
