﻿using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CheckClaimStatusSteps
    {
        [Given(@"I have the queue client without authorization")]
        public void GivenIHaveTheQueueClientWithoutAuthorization()
        {
            Responses.ErrorId = 0;
            Tools.QueueProviderClient = new QueueClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = ConfigurationClients.ConfigProviderBranch1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [When(@"I request the check claim status endpoint")]
        public void WhenIRequestTheCheckClaimStatusEndpoint()
        {
            Responses.QueueId.Should().NotBeNullOrEmpty("The claim should've created correctly");
            try
            {
                Responses.QueueStatus = Tools.QueueProviderClient.CheckQueueStatus(Responses.QueueId);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have the queue client without valid slug")]
        public void GivenIHaveTheQueueClientWithoutValidSlug()
        {
            Responses.ErrorId = 0;
            Tools.QueueProviderClient = new QueueClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigProviderBranch1.Authentication
            });
        }

        [Given(@"I have the queue client")]
        public void GivenIHaveTheQueueClient()
        {
            Responses.ErrorId = 0;
            Tools.QueueProviderClient = new QueueClient(ConfigurationClients.ConfigProviderBranch1);
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
                Responses.ErrorId = exception.ResponseCode;
            }

        }
    }
}
