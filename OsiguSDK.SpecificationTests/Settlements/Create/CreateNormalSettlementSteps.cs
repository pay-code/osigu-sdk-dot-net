using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Settlements.Models;
using OsiguSDK.SpecificationTests.Tools;
using Ploeh.AutoFixture;
using RestSharp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements.Create
{
    [Binding]
    public class CreateANewSettlementSteps
    {
        [Given(@"I have the settlements client")]
        public void GivenIHaveTheSettlementsClient()
        {
            try
            {
                TestClients.GenericRestClient = new GenericRestClient(ConfigurationClients.ConfigSettlement);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }
        
        [Given(@"I have the request data for a new normal settlement")]
        public void GivenIHaveTheRequestDataForANewNormalSettlement()
        {
            TestClients.Fixture.Customizations.Add(new StringBuilder());
            Requests.SettlementRequest = TestClients.Fixture.Create<Settlement>();
            Requests.SettlementRequest.To = Requests.SettlementRequest.From.AddMonths(1);
        }
        
        [When(@"I make the create normal settlement authorization request to the endpoint")]
        public void WhenIMakeTheCreateNormalSettlementAuthorizationRequestToTheEndpoint()
        {
            try
            {
                var a = TestClients.GenericRestClient.RequestToEndpoint<object>(Method.POST, "/settlements/normal", Requests.SettlementRequest);
                Responses.ErrorId = 204;
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            Responses.ErrorId.Should().Be(p0);
        }
    }
}
