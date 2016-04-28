using System;
using System.Linq;
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
        [Given(@"I have claims with amount less than (.*)")]
        public void GivenIHaveClaimsWithAmountLessThan(int p0)
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };
            
            CurrentData.Claims = claimTools.CreateManyRandomClaims(1);
        }

        [Given(@"I have entered a non retention provider")]
        public void GivenIHaveEnteredANonRetentionProvider()
        {
            Requests.NoRetentionProviderId = ConstantElements.NoRetentionProviderId;
        }

        [Given(@"I have entered a valid insurer")]
        public void GivenIHaveEnteredAValidInsurer()
        {
            Requests.InsurerId = ConstantElements.InsurerId;
        }

        [Given(@"I have the request data for a new cashout settlement")]
        public void GivenIHaveTheRequestDataForANewCashoutSettlement()
        {
            var claimsIds = CurrentData.Claims.Select(claim => new SettlementItemRequest
            {
                ClaimId = claim.Id.ToString()
            }).ToList();

            Requests.SettlementRequest = TestClients.Fixture.Build<SettlementRequest>()
                .With(x => x.From, DateTime.Now.AddMinutes(-1))
                .With(x => x.To, DateTime.Now)
                .With(x => x.ProviderId, Requests.NoRetentionProviderId.ToString())
                .With(x => x.InsurerId, Requests.InsurerId.ToString())
                .With(x => x.SettlementsItems, claimsIds)
                .Create();
        }
        
        [When(@"I make the request to the endpoint to create a new cashout")]
        public void WhenIMakeTheRequestToTheEndpointToCreateANewCashout()
        {
            Console.WriteLine("Settlement request " + Requests.SettlementRequest.Dump());

            try
            {
                TestClients.InternalRestClient.RequestToEndpoint(Method.POST, "/cashout", Requests.SettlementRequest);
                Responses.ErrorId = 201;
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

        }
        
        [When(@"I get the settlement created")]
        public void WhenIGetTheSettlementCreated()
        {
            var settlements = TestClients.InternalRestClient.RequestToEndpoint<Pagination<SettlementResponse>>(Method.GET).Content.ToList();
            Responses.Settlement = settlements.First(x => x.Id == settlements.Max(y => y.Id));
        }
        
        [Then(@"The calculation should be the expected")]
        public void ThenTheCalculationShouldBeTheExpected()
        {
            
        }
    }
}
