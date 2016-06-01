using System;
using System.Diagnostics;
using System.Threading;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class CheckExpressAuthorizationStatusSteps
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }
        public CheckExpressAuthorizationStatusSteps()
        {
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have created an express authorization")]
        public void GivenIHaveCreatedAnExpressAuthorization()
        {
            var request = new CreateExpressAuthorizationRequest
            {
                InsurerId = ConstantElements.InsurerId.ToString(),
                PolicyHolder = ConstantElements.PolicyHolder
            };
            
            Utils.Dump("CreateExpressAuthorizationRequest: ", request);

            try
            {
                Responses.QueueId = ExpressAuthorizationTool.CreateExpressAuthorization(request);
                Utils.Dump("QueueId: ", Responses.QueueId);

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Given(@"I have entered a valid queue id")]
        public void GivenIHaveEnteredAValidQueueId()
        {
            Responses.QueueId.Should().NotBeNullOrEmpty();
        }

        [When(@"I check the queue status")]
        public void WhenICheckTheQueueStatus()
        {
            Responses.ErrorId = 0;
            try
            {
                CheckQueueStatus();
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [Then(@"the resource should be created successfully")]
        public void ThenTheResourceShouldBeCreatedSuccessfully()
        {
            Responses.QueueStatus.ResourceId.Should().NotBeNullOrEmpty();
        }

        [Given(@"I have entered an invalid queue id")]
        public void GivenIHaveEnteredAnInvalidQueueId()
        {
            Responses.QueueId = "123";
        }

        [Then(@"the result should be not found")]
        public void ThenTheResultShouldBeNotFound()
        {
            Responses.ErrorId.Should().Be(404);
        }

        private static void CheckQueueStatus()
        {
            var contSeconds = 0;
            const int timeOutLimit = 30;
            var stopwatch = new Stopwatch();

            var queueClient = new QueueClient(ConfigurationClients.ConfigProviderBranch1);
            Responses.QueueStatus = queueClient.CheckQueueStatus(Responses.QueueId);

            stopwatch.Start();
            while (Responses.QueueStatus.ResourceId == null && contSeconds <= timeOutLimit)
            {
                contSeconds++;
                Thread.Sleep(1000);
                Responses.QueueStatus = queueClient.CheckQueueStatus(Responses.QueueId);
            }
            stopwatch.Stop();

            if (Responses.QueueStatus.ResourceId == null)
                throw new RequestException(
                    "The Timeout limit was exceeded when attempting get the express authorization with QueueId = " +
                    Responses.QueueId + ". Timeout Limit setted = " + timeOutLimit + " seconds.");

            var expressAuthorization =
                TestClients.ExpressAuthorizationClient.GetSingleExpressAuthorization(Responses.QueueStatus.ResourceId);

            Utils.Dump("Time elapsed for getting the authorizationId(" + expressAuthorization.Id + "): {0:hh\\:mm\\:ss}",
                stopwatch.Elapsed);
        }

    }
}
