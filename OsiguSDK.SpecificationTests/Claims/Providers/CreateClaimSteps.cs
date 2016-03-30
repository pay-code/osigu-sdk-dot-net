using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CreateClaimSteps
    {
        [Given(@"I have the provider claims client without authorization")]
        public void GivenIHaveTheProviderClaimsClientWithoutAuthorization()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = Tools.ConfigProviderBranch1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider claims client without valid slug")]
        public void GivenIHaveTheProviderClaimsClientWithoutValidSlug()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigProviderBranch1Development.Authentication
            });
        }

        [Given(@"I have the provider claims client")]
        public void GivenIHaveTheProviderClaimsClient()
        {
            Tools.ErrorId = 0;
            Tools.ClaimsProviderClient = new ClaimsClient(Tools.ConfigProviderBranch1Development);
        }

        [Given(@"I have the provider claims client two")]
        public void GivenIHaveTheProviderClaimsClientTwo()
        {
            Tools.ClaimsProviderClientWithNoPermission = new ClaimsClient(Tools.ConfigProviderBranch2Development);
        }

        [When(@"I request the create a claim endpoint with the second client")]
        public void WhenIRequestTheCreateAClaimEndpointWithTheSecondClient()
        {
            try
            {
                Tools.ClaimsProviderClientWithNoPermission.CreateClaim(Tools.AuthorizationId, Tools.CreateClaimRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request")]
        public void GivenTheCreateAClaimRequest()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Tools.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            for (var i = 0; i < 3; i++)
            {
                var item = Tools.Fixture.Create<CreateClaimRequest.Item>();
                item.ProductId = item.ProductId.Substring(0, 25);
                Tools.CreateClaimRequest.Items.Add(item);
            }

            Tools.AuthorizationId = "asdfasdfasdf";
        }
        
        [When(@"I request the create a claim endpoint")]
        public void WhenIRequestTheCreateAClaimEndpoint()
        {
            try
            {
                Tools.ClaimsProviderClient.CreateClaim("asdfadfasdf", Tools.CreateClaimRequest);
            }
            catch(RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"the create a claim request with an unexisting authorization")]
        public void GivenTheCreateAClaimRequestWithAnUnexistingAuthorization()
        {
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            Tools.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            for (var i = 0; i < 3; i++)
            {
                var item = Tools.Fixture.Create<CreateClaimRequest.Item>();
                item.ProductId = item.ProductId.Substring(0, 25);
                Tools.CreateClaimRequest.Items.Add(item);
            }

            Tools.AuthorizationId = "NotExistingAuth";
        }

        [Then(@"the result should be not existing")]
        public void ThenTheResultShouldBeNotExisting()
        {
            Tools.ErrorId.Should().Be(404);
        }

    }
}
