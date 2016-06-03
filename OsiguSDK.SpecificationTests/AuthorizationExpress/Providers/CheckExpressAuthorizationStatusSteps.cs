using FluentAssertions;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class CheckExpressAuthorizationStatusSteps
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }
        public ExpressAuthorizationHelper ExpressAuthorizationHelper { get; set; }
        public CheckExpressAuthorizationStatusSteps()
        {
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
            ExpressAuthorizationHelper = new ExpressAuthorizationHelper();
        }

        [Given(@"I have created an express authorization")]
        public void GivenIHaveCreatedAnExpressAuthorization()
        {
            Responses.QueueId = ExpressAuthorizationHelper.CreateExpressAuthorization();
        }

        [Given(@"I have entered a valid queue id")]
        public void GivenIHaveEnteredAValidQueueId()
        {
            Responses.QueueId.Should().NotBeNullOrEmpty();
        }

        [When(@"I check the queue status")]
        public void WhenICheckTheQueueStatus()
        {
            ExpressAuthorizationHelper.CheckExpressAuthorizationStatus(Responses.QueueId);
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
       
    }
}
