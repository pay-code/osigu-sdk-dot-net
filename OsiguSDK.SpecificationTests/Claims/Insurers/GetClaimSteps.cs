using System;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Insurers
{
    [Binding]
    public class GetAClaimSteps
    {
        [Given(@"I have the insurer claims client")]
        public void GivenIHaveTheInsurerClaimsClient()
        {
            TestClients.ClaimsInsurerClient = new ClaimsClient(ConfigurationClients.ConfigInsurer1);
        }
        
        [Given(@"I have the insurer claims client without authorization")]
        public void GivenIHaveTheInsurerClaimsClientWithoutAuthorization()
        {
            TestClients.ClaimsInsurerClient = new ClaimsClient(new Configuration
            {
                Authentication = new Authentication("noAuth"),
                BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                Slug = ConfigurationClients.ConfigInsurer1.Slug
            });
        }

        [Given(@"I have the insurer claim client without valid slug")]
        public void GivenIHaveTheInsurerClaimClientWithoutValidSlug()
        {
            TestClients.ClaimsInsurerClient = new ClaimsClient(new Configuration
            {
                Authentication = ConfigurationClients.ConfigInsurer1.Authentication,
                BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                Slug = "Another-slug"
            });
        }

        [When(@"I request the get a claim endpoint as insurer with an invalid claim id")]
        public void WhenIRequestTheGetAClaimEndpointAsInsurerWithAnInvalidClaimId()
        {
            Responses.QueueStatus.Should().NotBeNull("The queue should've returned the queue status");
            Responses.QueueStatus.ResourceId.Should().NotBeNullOrEmpty("The queue should've returned the queue status");

            try
            {
                Responses.InsurerClaim = TestClients.ClaimsInsurerClient.GetSingleClaim(Responses.Authorization.Id,
                    123456);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }
        
        [When(@"I request the get a claim endpoint as insurer")]
        public void WhenIRequestTheGetAClaimEndpointAsInsurer()
        {
            Responses.QueueStatus.Should().NotBeNull("The queue should've returned the queue status");
            Responses.QueueStatus.ResourceId.Should().NotBeNullOrEmpty("The queue should've returned the queue status");

            try
            {
                Responses.InsurerClaim = TestClients.ClaimsInsurerClient.GetSingleClaim(Responses.Authorization.Id,
                    int.Parse(Responses.QueueStatus.ResourceId));
            }
            catch (RequestException exception)

            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }
        
        [Then(@"the insurer claim should not be null")]
        public void ThenTheInsurerClaimShouldNotBeNull()
        {
            Responses.InsurerClaim.Should().NotBeNull();
        }
        
        [Then(@"the insurer claim should have the needed values")]
        public void ThenTheInsurerClaimShouldHaveTheNeededValues()
        {
            Responses.InsurerClaim.Id.Should().Be(int.Parse(Responses.QueueStatus.ResourceId));
            Responses.InsurerClaim.Copayment.Should().Be(Responses.Claim.Copayment);
            Responses.InsurerClaim.VerificationCode.Should().NotBeNullOrEmpty();
            Responses.InsurerClaim.Status.Should().Be("APPROVED");

            Responses.InsurerClaim.Items.Count.Should().Be(Requests.CreateClaimRequest.Items.Count);

            var claimItems = Responses.InsurerClaim.Items.Select(x => new
            {
                ProductId = x.ProductId.Substring(9),
                x.Quantity,
                x.SubstituteProductId
            });

            var expectedItems = Requests.CreateClaimRequest.Items.Select(x => new
            {
                ProductId = x.ProductId.Substring(10),
                x.Quantity,
                x.SubstituteProductId
            });

            claimItems.ShouldAllBeEquivalentTo(expectedItems);

            Responses.InsurerClaim.TotalCoInsurance.Should().Be(Responses.Claim.TotalCoInsurance);
            Responses.InsurerClaim.Invoice.Amount.Should().Be(Requests.Invoice.Amount);
        }
    }
}
