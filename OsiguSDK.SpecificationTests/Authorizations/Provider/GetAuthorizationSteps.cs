using System;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
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
            Tools.providerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1Development.BaseUrl,
                Slug = ConfigurationClients.ConfigProviderBranch1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the provider authorizations client with an invalid slug")]
        public void GivenIHaveTheProviderAuthorizationsClientWithAnInvalidSlug()
        {
            Tools.providerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigProviderBranch1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigProviderBranch1Development.Authentication
            });
        }

        [Given(@"I have the provider authorizations client")]
        public void GivenIHaveTheProviderAuthorizationsClient()
        {
            try
            {
                Tools.providerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigProviderBranch1Development);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"I have the provider authorizations client")]
        public void ThenIHaveTheProviderAuthorizationsClient()
        {
            try
            {
                Tools.providerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigProviderBranch1Development);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Given(@"I have an invalid authorization id for the provider")]
        public void GivenIHaveAnInvalidAuthorizationIdForTheProvider()
        {
            Tools.AuthorizationId = "1";
        }
        
        [When(@"I make the get authorization request as a provider to the endpoint")]
        public void WhenIMakeTheGetAuthorizationRequestAsAProviderToTheEndpoint()
        {
            try
            {
                authorizationResponse = Tools.providerAuthorizationClient.GetSingleAuthorization(Tools.AuthorizationId);
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
            //authorizationResponse.Id.Should().Be(Tools.AuthorizationId);
            authorizationResponse.AuthorizationDate.Date.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Date);
            authorizationResponse.AuthorizationDate.Hour.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Hour);
            authorizationResponse.ExpiresAt.Date.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Date);
            authorizationResponse.ExpiresAt.Hour.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Hour);
            //authorizationResponse.Policy.PolicyHolder.Id.Should().Be(Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id);
            authorizationResponse.Policy.Number.Should().Be(Tools.submitAuthorizationRequest.Policy.Number);
            authorizationResponse.Items.Count.Should().Be(Tools.submitAuthorizationRequest.Items.Count);
        }


    }
}
