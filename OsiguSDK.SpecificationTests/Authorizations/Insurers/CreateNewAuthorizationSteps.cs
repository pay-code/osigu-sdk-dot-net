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
            Tools.Fixture.Customizations.Add(new Tools.StringBuilder());
            Tools.submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest > ();
            DateTime authorizationDate = Tools.Fixture.Create<DateTime>();
            //Tools.submitAuthorizationRequest.AuthorizationDate =
            //    $"{authorizationDate:s}"+"Z";
            //Tools.submitAuthorizationRequest.ExpiresAt =
            //    $"{authorizationDate.AddMonths(1):s}" + "Z";
            //Tools.submitAuthorizationRequest.Policy.ExpirationDate =
            //    $"{Tools.Fixture.Create<DateTime>():s}" + "Z";
            //Tools.submitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth =
            //    $"{Tools.Fixture.Create<DateTime>():s}" + "Z";
            Tools.submitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            for (int pos = 0; pos < Tools.submitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.submitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociateProductId[pos];
            }

            //Tools.submitAuthorizationRequest.ReferenceId = Tools.submitAuthorizationRequest.ReferenceId.Substring(0, 24);
            //Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id =
            //    Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id.Substring(0, 24);
            //Tools.submitAuthorizationRequest.Doctor.MedicalLicense =
            //    Tools.submitAuthorizationRequest.Doctor.MedicalLicense.Substring(0, 24);
            //Tools.submitAuthorizationRequest.Policy.Certificate =
            //    Tools.submitAuthorizationRequest.Policy.Certificate.Substring(0, 24);
            //Tools.submitAuthorizationRequest.Policy.Number =
            //    Tools.submitAuthorizationRequest.Policy.Number.Substring(0, 24);
            
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
            Tools.submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            DateTime authorizationDate = Tools.Fixture.Create<DateTime>();
            //Tools.submitAuthorizationRequest.AuthorizationDate =
            //    $"{authorizationDate:s}" + "Z";
            //Tools.submitAuthorizationRequest.ExpiresAt =
            //    $"{authorizationDate.AddMonths(1):s}" + "Z";
            //Tools.submitAuthorizationRequest.Policy.ExpirationDate =
            //    $"{Tools.Fixture.Create<DateTime>():s}" + "Z";
            //Tools.submitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth =
            //    $"{Tools.Fixture.Create<DateTime>():s}" + "Z";
            Tools.submitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";

        }


        [When(@"I make the new authorization request to the endpoint")]
        public void WhenIMakeTheNewAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = Tools.insurerAuthorizationClient.CreateAuthorization(Tools.submitAuthorizationRequest);
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
            errorMessage.ResponseCode.Should().Be(403);
        }


        [Then(@"I have valid response for creating the authorization")]
        public void ThenIHaveValidResponseForCreatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(201);
        }

        [Then(@"the result should be unprocessable fot that request")]
        public void ThenTheResultShouldBeUnprocessableFotThatRequest()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }

    }
}
