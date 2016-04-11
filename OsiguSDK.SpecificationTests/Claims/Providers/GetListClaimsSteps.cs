using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using TechTalk.SpecFlow;
using OsiguSDK.Providers.Models;
using FluentAssertions;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class GetAListOfClaimsSteps
    {
        private Pagination<Claim> list = new Pagination<Claim>();
        [When(@"I request the get a list of claims endpoint")]
        public void WhenIRequestTheGetAListOfClaimsEndpoint()
        {
            try
            {
                list = Tools.ClaimsProviderClient.GetListOfClaims();
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Then(@"the list should have the same amount of claims as expected")]
        public void ThenTheListShouldHaveTheSameAmountOfClaimsAsExpected()
        {
            list.NumberOfElements.Should().Be(list.Content.Count);
        }
    }
}
