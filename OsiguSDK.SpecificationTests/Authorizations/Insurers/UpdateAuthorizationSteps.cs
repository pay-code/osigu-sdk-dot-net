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
                responseAuthorization = Tools.InsurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId, Tools.SubmitAuthorizationRequest);
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
                responseAuthorization = Tools.InsurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId, Tools.SubmitAuthorizationRequest);
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
            Tools.SubmitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Tools.SubmitAuthorizationRequest.ExpiresAt = Tools.SubmitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Tools.SubmitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.SubmitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.SubmitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Tools.SubmitAuthorizationRequest.ReferenceId = String.Empty;
            Tools.SubmitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            Tools.SubmitAuthorizationRequest.Policy.Certificate = String.Empty;
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
            responseAuthorization.ReferenceId.Should().NotBe(Tools.SubmitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Tools.SubmitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Tools.SubmitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Tools.SubmitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Tools.SubmitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Tools.SubmitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Tools.SubmitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Tools.SubmitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Tools.SubmitAuthorizationRequest.Items.Count);
        }


    }
}
