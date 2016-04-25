using System;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Authorizations.Provider
{
    [Binding]
    public class GetAnAuthorizationAsAProviderSteps
    {
        private Authorization authorizationResponse { get; set; }
        private RequestException errorMessage { get; set; }

         [Given(@"I have the provider authorizations client with an invalid token")]
        public void GivenIHaveTheProviderAuthorizationsClientWithAnInvalidToken()
        {
            TestClients.ProviderAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = ConfigurationClients.ConfigProviderBranch1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider authorizations client with an invalid slug")]
        public void GivenIHaveTheProviderAuthorizationsClientWithAnInvalidSlug()
        {
            TestClients.ProviderAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigProviderBranch1.Authentication
            });
        }

        [Given(@"I have the provider authorizations client")]
        public void GivenIHaveTheProviderAuthorizationsClient()
        {
            try
            {
                TestClients.ProviderAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigProviderBranch1);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"I have the provider authorizations client")]
        public void ThenIHaveTheProviderAuthorizationsClient()
        {
            try
            {
                TestClients.ProviderAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigProviderBranch1);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Given(@"I have an invalid authorization id for the provider")]
        public void GivenIHaveAnInvalidAuthorizationIdForTheProvider()
        {
            Responses.AuthorizationId = "1";
        }
        
        [When(@"I make the get authorization request as a provider to the endpoint")]
        public void WhenIMakeTheGetAuthorizationRequestAsAProviderToTheEndpoint()
        {
            try
            {
                authorizationResponse = TestClients.ProviderAuthorizationClient.GetSingleAuthorization(Responses.AuthorizationId);
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }
        
        [Then(@"the result should be forbidden for getting the authorization for the proivder")]
        public void ThenTheResultShouldBeForbiddenForGettingTheAuthorizationForTheProivder()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be not found for getting the authorization for the provider")]
        public void ThenTheResultShouldBeNotFoundForGettingTheAuthorizationForTheProvider()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }



        [Then(@"I have a valid response for getting the authorization for the provider")]
        public void ThenIHaveAValidResponseForGettingTheAuthorizationForTheProvider()
        {
            errorMessage.ResponseCode.Should().Be(200);
            //authorizationResponse.Id.Should().Be(TestClients.AuthorizationId);
            authorizationResponse.AuthorizationDate.Date.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Date);
            authorizationResponse.AuthorizationDate.Hour.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Hour);
            authorizationResponse.ExpiresAt.Date.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Date);
            authorizationResponse.ExpiresAt.Hour.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Hour);
            //authorizationResponse.Policy.PolicyHolder.Id.Should().Be(Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id);
            authorizationResponse.Policy.Number.Should().Be(Requests.SubmitAuthorizationRequest.Policy.Number);
            authorizationResponse.Items.Count.Should().Be(Requests.SubmitAuthorizationRequest.Items.Count);
        }


    }
}
