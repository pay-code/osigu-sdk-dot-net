using System;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;
using Ploeh.AutoFixture;
using RestSharp;
using ServiceStack.Text;

namespace OsiguSDK.SpecificationTests.Settlements.Calculation
{
    [Binding]
    public class ValidateTheCalculatiosOfSettementsSteps
    {
        public ISettlementCalculator SettlementCalculator { get; set; }
        
        [Given(@"I have (.*) claims with amount '(.*)'")]
        public void GivenIHaveClaimsWithAmount(int numberOfClaimsToCreate, string claimAmountRange)
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };
            
            CurrentData.Claims = claimTools.CreateManyRandomClaims(numberOfClaimsToCreate, Utils.ParseEnum<ClaimAmountRange>(claimAmountRange));

            Console.WriteLine("Claims details");
            Console.WriteLine(CurrentData.Claims.Select(x => new { x.Id, x.Invoice.Amount }).Dump());


        }

        [Given(@"I have entered a '(.*)'")]
        public void GivenIHaveEnteredA(string providerType)
        {
            Requests.IsAgentRetention = Convert.ToBoolean(Convert.ToInt16(Utils.ParseEnum<ProviderType>(providerType)));

            Requests.ProviderId = Requests.IsAgentRetention ? ConstantElements.RetentionProviderId : ConstantElements.NoRetentionProviderId;

        }

        [Given(@"I have entered a valid insurer")]
        public void GivenIHaveEnteredAValidInsurer()
        {
            Requests.InsurerId = ConstantElements.InsurerId;
        }
        
        [Given(@"I have the request data for a new settlement")]
        public void GivenIHaveTheRequestDataForANewSettlement()
        {
            Requests.InitialDate = DateTime.Now.AddMinutes(-1);
            Requests.EndDate = DateTime.Now;

            var claimsIds = CurrentData.Claims.Select(claim => new SettlementItemRequest
            {
                ClaimId = claim.Id.ToString()
            }).ToList();

            Requests.SettlementRequest = TestClients.Fixture.Build<SettlementRequest>()
                .With(x => x.From, Requests.InitialDate)
                .With(x => x.To, Requests.EndDate)
                .With(x => x.ProviderId, Requests.ProviderId.ToString())
                .With(x => x.InsurerId, Requests.InsurerId.ToString())
                .With(x => x.SettlementsItems, claimsIds)
                .Create();
        }

        [When(@"I make the request to the endpoint to create a new '(.*)'")]
        public void WhenIMakeTheRequestToTheEndpointToCreateANew(string settlementType)
        {
            Console.WriteLine("Settlement request " + Requests.SettlementRequest.Dump());

            try
            {
                var requestBody = Requests.EmptyBodyRequest ? new object() : Requests.SettlementRequest;
                Requests.EmptyBodyRequest = false;
                Requests.SettlementType = Utils.ParseEnum<SettlementType>(settlementType);
                Responses.Settlement = TestClients.InternalRestClient.RequestToEndpoint<SettlementResponse>(Method.POST, "/" + settlementType.ToLower(), requestBody);
                Responses.ErrorId = 201;

                SettlementTool.PrintSettlmentAsProvider(Responses.Settlement.Id, FormatSettlementPrint.PDF);

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
                Responses.ResponseMessage = exception.Message;
                Responses.Errors = exception.Errors;
            }
        }
        
        [When(@"I get the settlement created")]
        public void WhenIGetTheSettlementCreated()
        {
            Console.WriteLine("Settlement Created");
            Console.WriteLine(Responses.Settlement.Dump());
        }
        
        [Then(@"The calculation should be the expected")]
        public void ThenTheCalculationShouldBeTheExpected()
        {

            SettlementCalculator = new SettlementCalculator(Requests.IsAgentRetention, CurrentData.Claims,
                Requests.SettlementType);

            Console.WriteLine("Total Amount");
            Console.WriteLine(SettlementCalculator.GetTotalAmount().Dump());
            Console.WriteLine("Total Discount");
            Console.WriteLine(SettlementCalculator.GetTotalDiscount().Dump());
            Console.WriteLine("Total Taxes");
            Console.WriteLine(SettlementCalculator.GetTaxes().Dump());
            Console.WriteLine("Total Commissions");
            Console.WriteLine(SettlementCalculator.GetCommissions().Dump());
            Console.WriteLine("Total Retentions");
            Console.WriteLine(SettlementCalculator.GetRetentions().Dump());

            Responses.Settlement.TotalAmount.Should().Be(SettlementCalculator.GetTotalAmount());

            Responses.Settlement.TotalDiscount.Should().Be(SettlementCalculator.GetTotalDiscount());

            Responses.Settlement.Taxes.ShouldAllBeEquivalentTo(SettlementCalculator.GetTaxes(),
                x => x.Excluding(y => y.Id).Excluding(y => y.CreatedAt));

            var utcNow = DateTime.UtcNow;
            foreach (var tax in Responses.Settlement.Taxes)
            {
                tax.Id.Should().BeGreaterThan(0);
                tax.CreatedAt.Should().BeCloseTo(utcNow,30000) ;
            }

            Responses.Settlement.Comissions.ShouldAllBeEquivalentTo(SettlementCalculator.GetCommissions(),
                x=>x.Excluding(y=>y.Id).Excluding(y=>y.CreatedAt));

            foreach (var commission in Responses.Settlement.Comissions)
            {
                commission.Id.Should().BeGreaterThan(0);
                commission.CreatedAt.Should().BeCloseTo(utcNow, 30000);
            }
            
            Responses.Settlement.Retentions.ShouldAllBeEquivalentTo(SettlementCalculator.GetRetentions(),
                x=>x.Excluding(y=>y.Id).Excluding(y=>y.CreatedAt));

            foreach (var retention in Responses.Settlement.Retentions)
            {
                retention.Id.Should().BeGreaterThan(0);
                retention.CreatedAt.Should().BeCloseTo(utcNow, 30000);
            }

        }

        [Then(@"the result should be created")]
        public void ThenTheResultShouldBeCreated()
        {
            Responses.ErrorId.Should().Be(201);

        }

    }
}
