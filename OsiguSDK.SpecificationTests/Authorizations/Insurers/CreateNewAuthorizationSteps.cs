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
            ScenarioContext.Current.Pending();
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
            Tools.submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest > ();
            //Tools.submitAuthorizationRequest.AuthorizationDate =
            //new DateTime(Tools.submitAuthorizationRequest.AuthorizationDate.Year, Tools.submitAuthorizationRequest.AuthorizationDate.Month, Tools.submitAuthorizationRequest.AuthorizationDate.Day, Tools.submitAuthorizationRequest.AuthorizationDate.Hour, Tools.submitAuthorizationRequest.AuthorizationDate.Minute, Tools.submitAuthorizationRequest.AuthorizationDate.Second,);
            
Tools.submitAuthorizationRequest.ExpiresAt =
                Tools.submitAuthorizationRequest.AuthorizationDate.AddMonths(1).ToUniversalTime();
            Tools.submitAuthorizationRequest.ReferenceId = Tools.submitAuthorizationRequest.ReferenceId.Substring(0, 24);
            for (int pos = 0; pos < Tools.submitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.submitAuthorizationRequest.Items[pos].ProductId =
                    Tools.submitAuthorizationRequest.Items[pos].ProductId.Substring(0, 24);
            }
            Tools.submitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Doctor.MedicalLicense =
                Tools.submitAuthorizationRequest.Doctor.MedicalLicense.Substring(0, 24);
            Tools.submitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.Certificate = "0000011255";
            Tools.submitAuthorizationRequest.Policy.Number = "55222";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id =
                Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id.Substring(0, 24);
            Tools.submitAuthorizationRequest.Policy.ExpirationDate =
                Tools.submitAuthorizationRequest.Policy.ExpirationDate.ToUniversalTime();
            //Tools.submitAuthorizationRequest.Policy.PolicyHolder.Cellphone = "(734) 555-1212";
            //Tools.submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
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
            errorMessage.ResponseCode.Should().Be(404);
        }


        [Then(@"I have valid response for creating the authorization")]
        public void ThenIHaveValidResponseForCreatingTheAuthorization()
        {
            errorMessage.ResponseCode.Should().Be(201);
        }

    }
}
