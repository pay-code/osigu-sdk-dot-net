using System.Linq;
using System.Threading;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Tools;
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
                Responses.Claim = TestClients.ClaimsProviderClient.GetSingleClaim(Responses.QueueStatus.ResourceId);
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
                Responses.Claim = TestClients.ClaimsProviderClient.GetSingleClaim("1234567890");
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
            Responses.Claim.Copayment.Should().Be(0);
            Responses.Claim.VerificationCode.Should().NotBeNullOrEmpty();
            Responses.Claim.Status.Should().BeNull();

            Responses.Claim.Items.Count.Should().Be(Requests.CreateClaimRequest.Items.Count);

            var claimItems = Responses.Claim.Items.Select(x => new
            {
                x.ProductId,
                x.Quantity,
                x.SubstituteProductId
            });

            var expectedItems = Requests.CreateClaimRequest.Items.Select(x => new
            {
                x.ProductId,
                x.Quantity,
                x.SubstituteProductId
            });

            claimItems.ShouldAllBeEquivalentTo(expectedItems);
            
            Responses.Claim.TotalCoInsurance.Should().Be(0.20m*Responses.Claim.Items.Sum(x => x.Price*x.Quantity));
        }
    }
}
