using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Insurers
{
    [Binding]
    public class GetAListOfClaimsByAnInsurerSteps
    {
        private Pagination<Claim> _list = new Pagination<Claim>();
            
        [Given(@"I have the insurer claims client without valid slug")]
        public void GivenIHaveTheInsurerClaimsClientWithoutValidSlug()
        {
            TestClients.ClaimsInsurerClient = new ClaimsClient(ConfigurationClients.ConfigInsurer1);
        }
        
        [When(@"I request the get a list of claims as insurer endpoint")]
        public void WhenIRequestTheGetAListOfClaimsAsInsurerEndpoint()
        {
            ScenarioContext.Current.Pending();
            /*try
            {
                _list = TestClients.ClaimsInsurerClient.GetListOfClaims()
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }*/
        }

        [Then(@"the list should have the same amount of insurer claims as expected")]
        public void ThenTheListShouldHaveTheSameAmountOfInsurerClaimsAsExpected()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
