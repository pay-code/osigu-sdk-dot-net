using System.Threading;
using FluentAssertions;
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
            Responses.QueueStatus.Should().NotBeNull("The queue should've returned the queue status");
            Responses.QueueStatus.ResourceId.Should().NotBeNullOrEmpty("The queue should've returned the queue status");
            try
            {
                Responses.Claim = Tools.ClaimsProviderClient.GetSingleClaim(Responses.QueueStatus.ResourceId);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I delay the check status request")]
        public void WhenIDelayTheCheckStatusRequest()
        {
            Thread.Sleep(10000);
        }

        [When(@"I request the get a claim endpoint with an invalid claim id")]
        public void WhenIRequestTheGetAClaimEndpointWithAnInvalidClaimId()
        {
            Responses.QueueStatus.Should().NotBeNull("The queue should've returned the queue status");
            Responses.QueueStatus.ResourceId.Should().NotBeNullOrEmpty("The queue should've returned the queue status");
            try
            {
                Responses.Claim = Tools.ClaimsProviderClient.GetSingleClaim("1234567890");
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Then(@"the claim should not be null")]
        public void ThenTheClaimShouldNotBeNull()
        {
            Responses.Claim.Should().NotBeNull();
        }

        [Then(@"the claim should have the needed values")]
        public void ThenTheClaimShouldHaveTheNeededValues()
        {
            Responses.Claim.Id.Should().Be(int.Parse(Responses.QueueStatus.ResourceId));
            Responses.Claim.Items.Count.Should().Be(Requests.CreateClaimRequest.Items.Count);

            for (var i = 0; i < Responses.Claim.Items.Count; i++)
            {
                Responses.Claim.Items[i].ProductId.Should().Be(Requests.CreateClaimRequest.Items[i].ProductId);
                Responses.Claim.Items[i].Quantity.Should().Be(Requests.CreateClaimRequest.Items[i].Quantity);
                Responses.Claim.Items[i].SubstituteProductId.Should().Be(Requests.CreateClaimRequest.Items[i].SubstituteProductId);
            }

            
        }

    }
}
