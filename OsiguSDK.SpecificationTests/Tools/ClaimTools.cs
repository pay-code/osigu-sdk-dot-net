using OsiguSDK.Core.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.Insurers.Models;
using Ploeh.AutoFixture;
using OsiguSDK.Insurers.Models.Requests;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class ClaimTools
    {
        public IConfiguration ProviderBranchConfiguration { get; set; }
        public IConfiguration InsurerConfiguration { get; set; }

        public ClaimTools()
        {
        }

        public ClaimTools(IConfiguration providerBranchConfiguration, IConfiguration insurerConfiguration)
        {
            ProviderBranchConfiguration = providerBranchConfiguration;
            InsurerConfiguration = insurerConfiguration;
        }

        public Providers.Models.Claim CreateRandomClaim()
        {
            try
            {
                Responses.ErrorId = 0;

                TestClients.ClaimsProviderClient = new ClaimsClient(ProviderBranchConfiguration);

                TestClients.InsurerAuthorizationClient = new Insurers.Clients.AuthorizationsClient(InsurerConfiguration);

                TestClients.QueueProviderClient = new QueueClient(ProviderBranchConfiguration);

                SetAuthorizationRequestData();

                DoAuthotizationPost();

                SetClaimRequestData();

                Responses.QueueId = TestClients.ClaimsProviderClient.CreateClaim(Responses.Authorization.Id, Requests.CreateClaimRequest);

                Thread.Sleep(10000);

                Responses.QueueStatus = TestClients.QueueProviderClient.CheckQueueStatus(Responses.QueueId);

                Responses.Claim = TestClients.ClaimsProviderClient.GetSingleClaim(Responses.QueueStatus.ResourceId);


            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred when creating a claim.  " + ex);
            }

            return new Providers.Models.Claim();
        }

        public List<Providers.Models.Claim> CreateManyRandomClaims(int numberOfClaims)
        {
            var claims = new List<Providers.Models.Claim>();

            for (int i = 0; i < numberOfClaims; i++)
            {
                claims.Add(CreateRandomClaim());
            }

            return claims;
        }

        private void SetClaimRequestData()
        {
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
            Requests.CreateClaimRequest.Pin = Responses.Authorization.Pin;
        }

        private void DoAuthotizationPost()
        {
            var responseAuthorization = TestClients.InsurerAuthorizationClient.CreateAuthorization(Requests.SubmitAuthorizationRequest);
            Responses.Authorization = responseAuthorization;
        }

        private void SetAuthorizationRequestData()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Requests.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Requests.SubmitAuthorizationRequest.Items[pos].ProductId = ConstantElements.InsurerAssociatedProductId[pos];
            }
            Responses.Authorization = new Authorization { Id = "1" };
        }

        private void CreateValidAuthorizationRequest()
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

        private void GenerateItemList()
        {
            Requests.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
        }

        private void FillItemList()
        {
            var r = new Random();
            for (var i = 0; i < 3; i++)
            {
                Requests.CreateClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = r.Next(100, 10000) / 100m,
                    ProductId = ConstantElements.ProviderAssociateProductId[i],
                    Quantity = (r.Next(0, 1000) % 10) + 1
                });
            }
        }

    }


    [TestFixture]
    public class ClaimToolsTests
    {
        
        [Test]
        public void CanCreateClaim()
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            var claim = claimTools.CreateRandomClaim();

            claim.Should().NotBeNull();
        }


    }
}