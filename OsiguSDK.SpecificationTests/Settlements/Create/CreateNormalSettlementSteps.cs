using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Settlements.Models;
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
                Tools.RestClient = new RestClient(ConfigurationClients.ConfigSettlement);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }
        
        [Given(@"I have the request data for a new normal settlement")]
        public void GivenIHaveTheRequestDataForANewNormalSettlement()
        {
            Tools.Fixture.Customizations.Add(new StringBuilder());
            Requests.SettlementRequest = Tools.Fixture.Create<Settlement>();
            Requests.SettlementRequest.To = Requests.SettlementRequest.From.AddMonths(1);
        }
        
        [When(@"I make the create normal settlement authorization request to the endpoint")]
        public void WhenIMakeTheCreateNormalSettlementAuthorizationRequestToTheEndpoint()
        {
            try
            {
                var a = Tools.RestClient.RequestToEndpoint<object>(Method.POST, "/settlements/normal", Requests.SettlementRequest);
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
