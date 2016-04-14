using System;
using System.Linq;
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
            Tools.QueueStatus.Should().NotBeNull("The queue should've returned the queue status");
            Tools.QueueStatus.ResourceId.Should().NotBeNullOrEmpty("The queue should've returned the queue status");
            try
            {
                Tools.Claim = Tools.ClaimsProviderClient.GetSingleClaim(Tools.QueueStatus.ResourceId);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
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
            Tools.QueueStatus.Should().NotBeNull("The queue should've returned the queue status");
            Tools.QueueStatus.ResourceId.Should().NotBeNullOrEmpty("The queue should've returned the queue status");
            try
            {
                Tools.Claim = Tools.ClaimsProviderClient.GetSingleClaim("InvalidId");
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Then(@"the claim should not be null")]
        public void ThenTheClaimShouldNotBeNull()
        {
            Tools.Claim.Should().NotBeNull();
        }

        [Then(@"the claim should have the needed values")]
        public void ThenTheClaimShouldHaveTheNeededValues()
        {
            Tools.Claim.Id.Should().Be(int.Parse(Tools.QueueStatus.ResourceId));
            Tools.Claim.Items.Count.Should().Be(Tools.CreateClaimRequest.Items.Count);

            for (var i = 0; i < Tools.Claim.Items.Count; i++)
            {
                Tools.Claim.Items[i].ProductId.Should().Be(Tools.CreateClaimRequest.Items[i].ProductId);
                Tools.Claim.Items[i].Quantity.Should().Be(Tools.CreateClaimRequest.Items[i].Quantity);
                Tools.Claim.Items[i].SubstituteProductId.Should().Be(Tools.CreateClaimRequest.Items[i].SubstituteProductId);
            }

            
        }

    }
}
