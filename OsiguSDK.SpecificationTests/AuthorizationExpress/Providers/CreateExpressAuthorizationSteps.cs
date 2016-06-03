using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.Providers.Clients;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class CreateExpressAuthorizationSteps
    {
        [Given(@"I have the express authorization provider client")]
        public void GivenIHaveTheExpressAuthorizationProviderClient()
        {
            TestClients.ExpressAuthorizationClient = new ExpressAuthorizationClient(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have entered a valid policy holder")]
        public void GivenIHaveEnteredAValidPolicyHolder()
        {
            Requests.PolicyHolder = ConstantElements.PolicyHolder;
        }
        
        [Given(@"I have the request data for a new express authorization")]
        public void GivenIHaveTheRequestDataForANewExpressAuthorization()
        {
            Requests.CreateExpressAuthorizationRequest = new CreateExpressAuthorizationRequest
            {
                InsurerId = Requests.InsurerId == 0 ? null : Requests.InsurerId.ToString(),
                PolicyHolder = Requests.PolicyHolder
            };

            Utils.Dump("Create Express Authorization Request: ", Requests.CreateExpressAuthorizationRequest);
        }

        [Then(@"the result should be forbidden")]
        public void ThenTheResultShouldBeForbidden()
        {
            Responses.ErrorId.Should().Be(403);

        }

        [When(@"I make the new express authorization request to the endpoint")]
        public void WhenIMakeTheNewExpressAuthorizationRequestToTheEndpoint()
        {
            Responses.ErrorId = 0;
            Responses.QueueId = null;
            try
            {
                Responses.QueueId = TestClients.ExpressAuthorizationClient.CreateExpressAuthorization(Requests.CreateExpressAuthorizationRequest);
                Responses.ErrorId = 202;
                Utils.Dump("QueueId: ", Responses.QueueId);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

        }
        
        [Then(@"the result should be Accepted")]
        public void ThenTheResultShouldBeAccepted()
        {
            Responses.ErrorId.Should().Be(202);
        }

        [Then(@"the queue id is not null or empty")]
        public void ThenTheQueueIdIsNotNullOrEmpty()
        {
            Responses.QueueId.Should().NotBeNullOrEmpty();
        }


        [Given(@"I have entered an invalid policy holder '(.*)'")]
        public void GivenIHaveEnteredAnInvalidPolicyHolder(string policyHolderField)
        {
            Requests.PolicyHolder = ConstantElements.PolicyHolder;
            if (policyHolderField == "Id")
               Requests.PolicyHolder.Id = "1111111111111";
            else
                Requests.PolicyHolder.DateOfBirth = DateTime.Parse("01/01/1950"); 
        }

        [Given(@"I have not included a insurer")]
        public void GivenIHaveNotIncludedAInsurer()
        {
            Requests.InsurerId = 0;
        }

        [Given(@"I have not included a policy holder")]
        public void GivenIHaveNotIncludedAPolicyHolder()
        {
            Requests.PolicyHolder = null;
        }

        [Given(@"I have the express authorization provider client with invalid slug")]
        public void GivenIHaveTheExpressAuthorizationProviderClientWithInvalidSlug()
        {
            var configuration = ConfigurationClients.ConfigProviderBranch1;
            configuration.Slug = "xxxxxx";

            TestClients.ExpressAuthorizationClient = new ExpressAuthorizationClient(configuration);
        }

    }
}
