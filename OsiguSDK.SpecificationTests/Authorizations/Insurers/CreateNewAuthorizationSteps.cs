using System;
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
            Tools.InsurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                Slug = ConfigurationClients.ConfigInsurer1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the insurer authorizations client with an invalid slug")]
        public void GivenIHaveTheInsurerAuthorizationsClientWithAnInvalidSlug()
        {
            Tools.InsurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                Slug = "another_slug",
                Authentication = ConfigurationClients.ConfigInsurer1.Authentication
            });
        }


        [Given(@"I have the insurer authorizations client")]
        public void GivenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                Tools.InsurerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigInsurer1);
            }
            catch(Exception ex) { Console.WriteLine(ex.StackTrace);}
        }


        [Given(@"I have the request data for a new authorization")]
        public void GivenIHaveTheRequestDataForANewAuthorization()
        {
           CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Tools.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.SubmitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociatedProductId[pos];
            }
            Tools.AuthorizationId = "1";
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
            Tools.SubmitAuthorizationRequest.ReferenceId = String.Empty;
            Tools.SubmitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            Tools.SubmitAuthorizationRequest.Policy.Certificate = String.Empty;
        }


        [Given(@"I have the request data for a new authorization with a duplicate product")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithADuplicateProduct()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Tools.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.SubmitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociatedProductId[0];
            }
        }


        [Then(@"I have the insurer authorizations client")]
        public void ThenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                Tools.InsurerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigInsurer1);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"I have the request data for a new authorization")]
        public void ThenIHaveTheRequestDataForANewAuthorization()
        {
          CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Tools.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.SubmitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociatedProductId[pos];
            }
            
        }


        [When(@"I make the new authorization request to the endpoint")]
        public void WhenIMakeTheNewAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.InsurerAuthorizationClient.CreateAuthorization(Tools.SubmitAuthorizationRequest);
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
            responseAuthorization.ReferenceId.Should().Be(Tools.SubmitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Tools.SubmitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Tools.SubmitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Tools.SubmitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Tools.SubmitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Tools.SubmitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Tools.SubmitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Tools.SubmitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Tools.SubmitAuthorizationRequest.Items.Count);
        }

        [Then(@"the result should be unprocessable fot that request")]
        public void ThenTheResultShouldBeUnprocessableFotThatRequest()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }

        public void CreateValidAuthorizationRequest()
        {
            Tools.Fixture.Customizations.Add(new StringBuilder());
            Tools.SubmitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Tools.SubmitAuthorizationRequest.ExpiresAt = Tools.SubmitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Tools.SubmitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.SubmitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.SubmitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Tools.SubmitAuthorizationRequest.Policy.PolicyHolder.Id = Tools.RPNTestPolicyNumber;
            Tools.SubmitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = Tools.RPNTestPolicyBirthday;
        }
    }
}
