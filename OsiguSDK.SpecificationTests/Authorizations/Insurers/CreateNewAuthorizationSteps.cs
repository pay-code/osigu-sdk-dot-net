using System;
using FluentAssertions;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using OsiguSDK.SpecificationTests.Tools.TestingProducts;
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
            TestClients.InsurerAuthorizationClient = new AuthorizationsClient(new Configuration
            {
                BaseUrl = ConfigurationClients.ConfigInsurer1.BaseUrl,
                Slug = ConfigurationClients.ConfigInsurer1.Slug,
                Authentication = new Authentication("noOAuthToken :(")
            });
        }

        [Given(@"I have the insurer authorizations client with an invalid slug")]
        public void GivenIHaveTheInsurerAuthorizationsClientWithAnInvalidSlug()
        {
            TestClients.InsurerAuthorizationClient = new AuthorizationsClient(new Configuration
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
                TestClients.InsurerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigInsurer1);
            }
            catch(Exception ex) { Console.WriteLine(ex.StackTrace);}
        }


        [Given(@"I have the request data for a new authorization")]
        public void GivenIHaveTheRequestDataForANewAuthorization()
        {
           CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Requests.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Requests.SubmitAuthorizationRequest.Items[pos].ProductId = Provider1Products.InsurerAssociatedProductId[pos];
            }
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
            Requests.SubmitAuthorizationRequest.ReferenceId = String.Empty;
            Requests.SubmitAuthorizationRequest.Doctor.MedicalLicense = String.Empty;
            Requests.SubmitAuthorizationRequest.Policy.Certificate = String.Empty;
        }


        [Given(@"I have the request data for a new authorization with a duplicate product")]
        public void GivenIHaveTheRequestDataForANewAuthorizationWithADuplicateProduct()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Requests.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Requests.SubmitAuthorizationRequest.Items[pos].ProductId = Provider1Products.InsurerAssociatedProductId[0];
            }
        }


        [Then(@"I have the insurer authorizations client")]
        public void ThenIHaveTheInsurerAuthorizationsClient()
        {
            try
            {
                TestClients.InsurerAuthorizationClient = new AuthorizationsClient(ConfigurationClients.ConfigInsurer1);
            }
            catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        [Then(@"I have the request data for a new authorization")]
        public void ThenIHaveTheRequestDataForANewAuthorization()
        {
          CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Requests.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Requests.SubmitAuthorizationRequest.Items[pos].ProductId = Provider1Products.InsurerAssociatedProductId[pos];
            }
            
        }


        [When(@"I make the new authorization request to the endpoint")]
        public void WhenIMakeTheNewAuthorizationRequestToTheEndpoint()
        {
            try
            {
                responseAuthorization = TestClients.InsurerAuthorizationClient.CreateAuthorization(Requests.SubmitAuthorizationRequest);
                Responses.Authorization = responseAuthorization;
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
            responseAuthorization.ReferenceId.Should().Be(Requests.SubmitAuthorizationRequest.ReferenceId);
            responseAuthorization.AuthorizationDate.Date.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Date);
            responseAuthorization.AuthorizationDate.Hour.Should().Be(Requests.SubmitAuthorizationRequest.AuthorizationDate.Hour);
            responseAuthorization.ExpiresAt.Date.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Date);
            responseAuthorization.ExpiresAt.Hour.Should().Be(Requests.SubmitAuthorizationRequest.ExpiresAt.Hour);
            responseAuthorization.Diagnoses.ShouldBeEquivalentTo(Requests.SubmitAuthorizationRequest.Diagnoses);
            responseAuthorization.Policy.PolicyHolder.Id.Should().Be(Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id);
            responseAuthorization.Policy.Number.Should().Be(Requests.SubmitAuthorizationRequest.Policy.Number);
            responseAuthorization.Items.Count.Should().Be(Requests.SubmitAuthorizationRequest.Items.Count);
        }

        [Then(@"the result should be unprocessable fot that request")]
        public void ThenTheResultShouldBeUnprocessableFotThatRequest()
        {
            errorMessage.ResponseCode.Should().Be(422);
        }

        public void CreateValidAuthorizationRequest()
        {
            TestClients.Fixture.Customizations.Add(new StringBuilder());
            Requests.SubmitAuthorizationRequest = TestClients.Fixture.Create<CreateAuthorizationRequest>();
            Requests.SubmitAuthorizationRequest.ExpiresAt = Requests.SubmitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Requests.SubmitAuthorizationRequest.Doctor.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id = ConstantElements.RPNTestPolicyNumber;
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = ConstantElements.RPNTestPolicyBirthday;
        }
    }
}
