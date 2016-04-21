using System;
using FluentAssertions;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;
using TechTalk.SpecFlow;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using Ploeh.AutoFixture;


namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CheckClaimStatusSteps
    {
        [Given(@"I have the queue client without authorization")]
        public void GivenIHaveTheQueueClientWithoutAuthorization()
        {
            Tools.ErrorId = 0;
            Tools.QueueProviderClient = new QueueClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = Tools.ConfigProviderBranch1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [When(@"I request the check claim status endpoint")]
        public void WhenIRequestTheCheckClaimStatusEndpoint()
        {
            Tools.QueueId.Should().NotBeNullOrEmpty("The claim should've created correctly");
            try
            {
                Tools.QueueStatus = Tools.QueueProviderClient.CheckQueueStatus(Tools.QueueId);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have the queue client without valid slug")]
        public void GivenIHaveTheQueueClientWithoutValidSlug()
        {
            Tools.ErrorId = 0;
            Tools.QueueProviderClient = new QueueClient(new Configuration
            {
                BaseUrl = Tools.ConfigProviderBranch1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigProviderBranch1Development.Authentication
            });
        }

        [Given(@"I have the queue client")]
        public void GivenIHaveTheQueueClient()
        {
            Tools.ErrorId = 0;
            Tools.QueueProviderClient = new QueueClient(Tools.ConfigProviderBranch1Development);
        }

        [When(@"I request the check claim status endpoint with an invalid queue id")]
        public void WhenIRequestTheCheckClaimStatusEndpointWithAnInvalidQueueId()
        {
            try
            {
                Tools.QueueProviderClient.CheckQueueStatus("Invalid_Id");
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }

        }
    }
}
