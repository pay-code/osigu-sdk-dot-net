using System;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
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
            insurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1Development.BaseUrl,
                Slug = ConfigurationClients.ConfigInsurer1Development.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the insurer authorizations client with an invalid slug")]
        public void GivenIHaveTheInsurerAuthorizationsClientWithAnInvalidSlug()
        {
            insurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1Development.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigInsurer1Development.Authentication
            });
        }


        [Given(@"I have the insurer authorizations client")]
        public void GivenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                insurerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigInsurer1Development);
            }
            catch(Exception ex) { Console.WriteLine(ex.StackTrace);}
        }


        [Given(@"I have the request data for a new authorization")]
        public void GivenIHaveTheRequestDataForANewAuthorization()
        {
           CreateValidAuthorizationRequest();
            for (int pos = 0; pos < submitAuthorizationRequest.Items.Count; pos++)
            {
                submitAuthorizationRequest.Items[pos].ProductId = InsurerAssociateProductId[pos];
            }
            AuthorizationId = "1";
        }

        [Given(@"I have the request data for a new authorization with an unreferenced product")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithAnUnreferencedProduct()
        {
           CreateValidAuthorizationRequest();
        }

        [Given(@"I have the request data for a new authorization with empty fields")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithEmptyFields()
        {
            CreateValidAuthorizationRequest();
            submitAuthorizationRequest.ReferenceId = String.Empty;
            submitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            submitAuthorizationRequest.Policy.Certificate = String.Empty;
        }


        [Given(@"I have the request data for a new authorization with a duplicate product")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithADuplicateProduct()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < submitAuthorizationRequest.Items.Count; pos++)
            {
                submitAuthorizationRequest.Items[pos].ProductId = InsurerAssociateProductId[0];
            }
        }


        [Then(@"I have the insurer authorizations client")]
        public void ThenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                insurerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigInsurer1Development);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"I have the request data for a new authorization")]
        public void ThenIHaveTheRequestDataForANewAuthorization()
        {
          CreateValidAuthorizationRequest();
            for (int pos = 0; pos < submitAuthorizationRequest.Items.Count; pos++)
            {
                submitAuthorizationRequest.Items[pos].ProductId = InsurerAssociateProductId[pos];
            }
            
        }


        [When(@"I make the new authorization request to the endpoint")]
        public void WhenIMakeTheNewAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = insurerAuthorizationClient.CreateAuthorization(submitAuthorizationRequest);
                AuthorizationId = responseAuthorization.Id;
                PIN = responseAuthorization.Pin;
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
            responseAuthorization.ReferenceId.Should().Be(submitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(submitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(submitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(submitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(submitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(submitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(submitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(submitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(submitAuthorizationRequest.Items.Count);
        }

        [Then(@"the result should be unprocessable fot that request")]
        public void ThenTheResultShouldBeUnprocessableFotThatRequest()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }

        public void CreateValidAuthorizationRequest()
        {
            Tools.Fixture.Customizations.Add(new StringBuilder());
            submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            submitAuthorizationRequest.ExpiresAt = submitAuthorizationRequest.AuthorizationDate.AddDays(1);
            submitAuthorizationRequest.Doctor.CountryCode = "GT";
            submitAuthorizationRequest.Policy.CountryCode = "GT";
            submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            submitAuthorizationRequest.Policy.PolicyHolder.Id = RPNTestPolicyNumber;
            submitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = RPNTestPolicyBirthday;
        }
    }
}
