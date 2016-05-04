using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Models;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class GetAListOfClaimsSteps
    {
        private Pagination<Claim> _list = new Pagination<Claim>();
        [When(@"I request the get a list of claims endpoint")]
        public void WhenIRequestTheGetAListOfClaimsEndpoint()
        {
            try
            {
                _list = TestClients.ClaimsProviderClient.GetListOfClaims();
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Then(@"the list should have the same amount of claims as expected")]
        public void ThenTheListShouldHaveTheSameAmountOfClaimsAsExpected()
        {
            _list.NumberOfElements.Should().Be(_list.Content.Count);
        }
    }
}
