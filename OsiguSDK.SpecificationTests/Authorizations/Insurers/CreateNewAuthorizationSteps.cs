using System;
using System.Runtime.ExceptionServices;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Authorizations.Insurers
{
    [Binding]
    public class CreateANewAuthorizationAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private Authorization responseAuthorization { get; set; }

        [Given(@"I have the insurer authorizations client with an invalid token")]
        public void GivenIHaveTheInsurerAuthorizationsClientWithAnInvalidToken()
        {
            Tools.insurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = Tools.ConfigInsurer1Development.BaseUrl,
                Slug = Tools.ConfigInsurer1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the insurer authorizations client with an invalid slug")]
        public void GivenIHaveTheInsurerAuthorizationsClientWithAnInvalidSlug()
        {
            Tools.insurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = Tools.ConfigInsurer1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = Tools.ConfigInsurer1Development.Authentication
            });
        }


        [Given(@"I have the insurer authorizations client")]
        public void GivenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                Tools.insurerAuthorizationClient = new AuthorizationsClient(Tools.ConfigInsurer1Development);
            }
            catch(Exception ex) { Console.WriteLine(ex.StackTrace);}
        }


        [Given(@"I have the request data for a new authorization")]
        public void GivenIHaveTheRequestDataForANewAuthorization()
        {
           CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Tools.submitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.submitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociateProductId[pos];
            }
            Tools.AuthorizationId = "1";
        }

        [Given(@"I have the request data for a new authorization with an unreferenced product")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithAnUnreferencedProduct()
        {
           CreateValidAuthorizationRequest();
        }

        [Given(@"I have the request data for a new authorization with a duplicate product")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithADuplicateProduct()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Tools.submitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.submitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociateProductId[0];
            }
        }


        [Then(@"I have the insurer authorizations client")]
        public void ThenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                Tools.insurerAuthorizationClient = new AuthorizationsClient(Tools.ConfigInsurer1Development);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"I have the request data for a new authorization")]
        public void ThenIHaveTheRequestDataForANewAuthorization()
        {
          CreateValidAuthorizationRequest();

        }


        [When(@"I make the new authorization request to the endpoint")]
        public void WhenIMakeTheNewAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.insurerAuthorizationClient.CreateAuthorization(Tools.submitAuthorizationRequest);
                Tools.AuthorizationId = responseAuthorization.Id;
                Tools.PIN = responseAuthorization.Pin;
                errorMessage = new RequestException("ok", 201);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Then(@"the result should be forbidden for that request")]
        public void ThenTheResultShouldBeForbiddenForThatRequest()
        {
       
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be unauthorized for that request")]
        public void ThenTheResultShouldBeUnauthorizedForThatRequest()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }


        [Then(@"I have valid response for creating the authorization")]
        public void ThenIHaveValidResponseForCreatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(201);
            responseAuthorization.ReferenceId.Should().Be(Tools.submitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Tools.submitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Tools.submitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.ShouldBeEquivalentTo(Tools.submitAuthorizationRequest.Items);
        }

        [Then(@"the result should be unprocessable fot that request")]
        public void ThenTheResultShouldBeUnprocessableFotThatRequest()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }

        public void CreateValidAuthorizationRequest()
        {
            Tools.Fixture.Customizations.Add(new Tools.StringBuilder());
            Tools.submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Tools.submitAuthorizationRequest.ExpiresAt = Tools.submitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Tools.submitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
        }
    }
}
