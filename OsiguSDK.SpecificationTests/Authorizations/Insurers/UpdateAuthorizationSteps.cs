using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Authorizations.Insurers
{
    [Binding]
    public class UpdateAnAuthorizationAsAnInsurerSteps
    {
        private RequestException errorMessage { get; set; }
        private Authorization responseAuthorization { get; set; }
        [When(@"I make the update authorization request to the endpoint")]
        public void WhenIMakeTheUpdateAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.InsurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId, Requests.SubmitAuthorizationRequest);
                Tools.AuthorizationId = responseAuthorization.Id;
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Then(@"I make the update authorization request to the endpoint")]
        public void ThenIMakeTheUpdateAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.InsurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId, Requests.SubmitAuthorizationRequest);
                Tools.AuthorizationId = responseAuthorization.Id;
                errorMessage = new RequestException("ok", 200);
            }
            catch (RequestException exception)
            {
                errorMessage = exception;
            }
        }

        [Then(@"I have the request data for a new authorization with empty fields")]
        public void ThenIHaveTheRequestDataForANewAuthorizationWithEmptyFields()
        {
            Tools.Fixture.Customizations.Add(new StringBuilder());
            Requests.SubmitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Requests.SubmitAuthorizationRequest.ExpiresAt = Requests.SubmitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Requests.SubmitAuthorizationRequest.Doctor.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Requests.SubmitAuthorizationRequest.ReferenceId = String.Empty;
            Requests.SubmitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            Requests.SubmitAuthorizationRequest.Policy.Certificate = String.Empty;
        }


        [Then(@"the result should be forbidden for updating the authorization")]
        public void ThenTheResultShouldBeForbiddenForUpdatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(403);
        }

        [Then(@"the result should be not found for updating the authorization")]
        public void ThenTheResultShouldBeNotFoundForUpdatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(404);
        }

        [Then(@"the result should be unprocesable for updating the authorization")]
        public void ThenTheResultShouldBeUnprocesableForUpdatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }


        [Then(@"I a valid response for updating the authorization")]
        public void ThenIAValidResponseForUpdatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(200);
            responseAuthorization.ReferenceId.Should().NotBe(Requests.SubmitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Requests.SubmitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Requests.SubmitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Requests.SubmitAuthorizationRequest.Items.Count);
        }


    }
}
