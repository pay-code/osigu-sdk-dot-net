using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
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
                responseAuthorization = insurerAuthorizationClient.ModifyAuthorization(AuthorizationId,submitAuthorizationRequest);
                AuthorizationId = responseAuthorization.Id;
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
                responseAuthorization = insurerAuthorizationClient.ModifyAuthorization(AuthorizationId, submitAuthorizationRequest);
                AuthorizationId = responseAuthorization.Id;
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
            submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            submitAuthorizationRequest.ExpiresAt = submitAuthorizationRequest.AuthorizationDate.AddDays(1);
            submitAuthorizationRequest.Doctor.CountryCode = "GT";
            submitAuthorizationRequest.Policy.CountryCode = "GT";
            submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            submitAuthorizationRequest.ReferenceId = String.Empty;
            submitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            submitAuthorizationRequest.Policy.Certificate = String.Empty;
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
            responseAuthorization.ReferenceId.Should().NotBe(submitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(submitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(submitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(submitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(submitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(submitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(submitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(submitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(submitAuthorizationRequest.Items.Count);
        }


    }
}
