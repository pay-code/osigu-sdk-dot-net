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
                responseAuthorization = Tools.insurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId, Tools.submitAuthorizationRequest);
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
                responseAuthorization = Tools.insurerAuthorizationClient.ModifyAuthorization(Tools.AuthorizationId, Tools.submitAuthorizationRequest);
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
            Tools.Fixture.Customizations.Add(new Tools.StringBuilder());
            Tools.submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Tools.submitAuthorizationRequest.ExpiresAt = Tools.submitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Tools.submitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Tools.submitAuthorizationRequest.ReferenceId = String.Empty;
            Tools.submitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            Tools.submitAuthorizationRequest.Policy.Certificate = String.Empty;
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
            responseAuthorization.ReferenceId.Should().NotBe(Tools.submitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Tools.submitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Tools.submitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Tools.submitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Tools.submitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Tools.submitAuthorizationRequest.Items.Count);
        }


    }
}
