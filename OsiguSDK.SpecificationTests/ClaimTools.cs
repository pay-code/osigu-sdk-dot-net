using OsiguSDK.Core.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using OsiguSDK.Insurers.Models.Requests;

namespace OsiguSDK.SpecificationTests
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

                Tools.ClaimsProviderClient = new ClaimsClient(ProviderBranchConfiguration);

                Tools.InsurerAuthorizationClient = new Insurers.Clients.AuthorizationsClient(InsurerConfiguration);

                Tools.QueueProviderClient = new QueueClient(ProviderBranchConfiguration);

                SetAuthorizationRequestData();

                DoAuthotizationPost();

                SetClaimRequestData();

                Responses.QueueId = Tools.ClaimsProviderClient.CreateClaim(Responses.AuthorizationId, Requests.CreateClaimRequest);

                Thread.Sleep(10000);

                Responses.QueueStatus = Tools.QueueProviderClient.CheckQueueStatus(Responses.QueueId);

                Responses.Claim = Tools.ClaimsProviderClient.GetSingleClaim(Responses.QueueStatus.ResourceId);


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
            Requests.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
            Requests.CreateClaimRequest.Pin = Responses.PIN;
        }

        private void DoAuthotizationPost()
        {
            var responseAuthorization = Tools.InsurerAuthorizationClient.CreateAuthorization(Requests.SubmitAuthorizationRequest);
            Responses.AuthorizationId = responseAuthorization.Id;
            Responses.PIN = responseAuthorization.Pin;
        }

        private void SetAuthorizationRequestData()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Requests.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Requests.SubmitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociatedProductId[pos];
            }
            Responses.AuthorizationId = "1";
        }

        private void CreateValidAuthorizationRequest()
        {
            Tools.Fixture.Customizations.Add(new StringBuilder());
            Requests.SubmitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Requests.SubmitAuthorizationRequest.ExpiresAt = Requests.SubmitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Requests.SubmitAuthorizationRequest.Doctor.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id = Tools.RPNTestPolicyNumber;
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = Tools.RPNTestPolicyBirthday;
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
                    ProductId = Tools.ProviderAssociateProductId[i],
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