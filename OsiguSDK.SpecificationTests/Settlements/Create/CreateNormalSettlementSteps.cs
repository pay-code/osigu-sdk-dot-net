using System;
using System.Collections.Generic;
using FluentAssertions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Tools;
using Ploeh.AutoFixture;
using ServiceStack.Text;
using TechTalk.SpecFlow;
using Claim = OsiguSDK.Providers.Models.Claim;

namespace OsiguSDK.SpecificationTests.Settlements.Create
{
    [Binding]
    public class CreateANewSettlementSteps
    {
        [Given(@"I have entered a valid provider")]
        public void GivenIHaveEnteredAValidProvider()
        {
            Requests.ProviderId = ConstantElements.RetentionProviderId;
        }

        [Given(@"I have entered an invalid insurer")]
        public void GivenIHaveEnteredAnInvalidInsurer()
        {
            Requests.InsurerId = 999999;
        }

        [Given(@"I have entered an invalid provider")]
        public void GivenIHaveEnteredAnInvalidProvider()
        {
            Requests.ProviderId = 999999;
        }

        [Then(@"the result should be bad request")]
        public void ThenTheResultShouldBeBadRequest()
        {
            Responses.ErrorId.Should().Be(400);
        }


        [Then(@"the message should be '(.*)'")]
        public void ThenTheMessageShouldBe(string responseMessage)
        {
            var message = ReplaceWildCards(responseMessage);
            Responses.ResponseMessage.Should().Be(message);
        }

        [Given(@"I have entered a empty list of claims")]
        public void GivenIHaveEnteredAEmptyListOfClaims()
        {
            CurrentData.Claims = new List<Claim>();
        }

        [Given(@"I have entered a list of claims invalid")]
        public void GivenIHaveEnteredAListOfClaimsInvalid()
        {
            var claim = TestClients.Fixture.Build<Claim>()
                .With(x => x.Id, 99999999)
                .Create();
            CurrentData.Claims = new List<Claim>{claim};
        }

        [Given(@"I have entered valid dates")]
        public void GivenIHaveEnteredValidDates()
        {
            Requests.InitialDate = DateTime.Now.AddMinutes(-1);
            Requests.EndDate = DateTime.Now;
        }

        [Given(@"I have entered invalid dates")]
        public void GivenIHaveEnteredInvalidDates()
        {
            Requests.InitialDate = DateTime.Now;
            Requests.EndDate = DateTime.Now.AddDays(-7);
        }
        
        [Then(@"the error list should be '(.*)'")]
        public void ThenTheErrorListShouldBe(string errorList)
        {
            var expectedErrors = JsonSerializer.DeserializeFromString<List<RequestError.ValidationError>>(errorList);
            expectedErrors.ShouldAllBeEquivalentTo(Responses.Errors);
        }

        [Given(@"I have an invalid request data for a new settlement")]
        public void GivenIHaveAnInvalidRequestDataForANewSettlement()
        {
            Requests.SettlementRequest = null;
        }

        [Given(@"I have an empty request data for a new settlement")]
        public void GivenIHaveAnEmptyRequestDataForANewSettlement()
        {
            Requests.EmptyBodyRequest = true;
        }

        private string ReplaceWildCards(string responseMessage)
        {
            if (responseMessage.Contains("{insurerId}"))
                responseMessage = responseMessage.Replace("{insurerId}", Requests.InsurerId.ToString());

            if (responseMessage.Contains("{providerId}"))
                responseMessage = responseMessage.Replace("{providerId}", Requests.ProviderId.ToString());
            
            return responseMessage;
        }
    }
}
