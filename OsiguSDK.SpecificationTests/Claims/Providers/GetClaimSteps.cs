using System;
using OsiguSDK.Core.Exceptions;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class GetAClaimSteps
    {
        [When(@"I request the get a claim endpoint")]
        public void WhenIRequestTheGetAClaimEndpoint()
        {
            try
            {
                Tools.Claim = Tools.ClaimsProviderClient.GetSingleClaim(Tools.QueueStatus.ResourceId);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the get a claim endpoint with an invalid claim id")]
        public void WhenIRequestTheGetAClaimEndpointWithAnInvalidClaimId()
        {
            try
            {
                Tools.Claim = Tools.ClaimsProviderClient.GetSingleClaim("InvalidId");
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }


    }
}
