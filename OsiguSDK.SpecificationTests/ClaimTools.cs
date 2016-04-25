using OsiguSDK.Core.Config;
using System;
using System.Collections.Generic;
using System.Threading;
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
                Tools.ErrorId = 0;

                Tools.ClaimsProviderClient = new Providers.Clients.ClaimsClient(ProviderBranchConfiguration);

                Tools.insurerAuthorizationClient = new Insurers.Clients.AuthorizationsClient(InsurerConfiguration);

                Tools.QueueProviderClient = new QueueClient(ProviderBranchConfiguration);

                SetAuthorizationRequestData();

                DoAuthotizationPost();

                SetClaimRequestData();

                Tools.QueueId = Tools.ClaimsProviderClient.CreateClaim(Tools.AuthorizationId, Tools.CreateClaimRequest);

                Thread.Sleep(10000);

                Tools.QueueStatus = Tools.QueueProviderClient.CheckQueueStatus(Tools.QueueId);

                Tools.Claim = Tools.ClaimsProviderClient.GetSingleClaim(Tools.QueueStatus.ResourceId);


            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred when creating a claim.  " + ex);
            }

            return new Providers.Models.Claim();
        }

        public List<Providers.Models.Claim> CreateRandomListOfClaims(int numberOfClaims)
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
            Tools.CreateClaimRequest = Tools.Fixture.Create<CreateClaimRequest>();
            GenerateItemList();
            Tools.CreateClaimRequest.Pin = Tools.PIN;
        }

        private void DoAuthotizationPost()
        {
            var responseAuthorization = Tools.insurerAuthorizationClient.CreateAuthorization(Tools.submitAuthorizationRequest);
            Tools.AuthorizationId = responseAuthorization.Id;
            Tools.PIN = responseAuthorization.Pin;
        }

        private void SetAuthorizationRequestData()
        {
            CreateValidAuthorizationRequest();
            for (int pos = 0; pos < Tools.submitAuthorizationRequest.Items.Count; pos++)
            {
                Tools.submitAuthorizationRequest.Items[pos].ProductId = Tools.InsurerAssociateProductId[pos];
            }
            Tools.AuthorizationId = "1";
        }

        private void CreateValidAuthorizationRequest()
        {
            Tools.Fixture.Customizations.Add(new Tools.StringBuilder());
            Tools.submitAuthorizationRequest = Tools.Fixture.Create<CreateAuthorizationRequest>();
            Tools.submitAuthorizationRequest.ExpiresAt = Tools.submitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Tools.submitAuthorizationRequest.Doctor.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.CountryCode = "GT";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.Id = Tools.RPNTestPolicyNumber;
            Tools.submitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = Tools.RPNTestPolicyBirthday;
        }

        private void GenerateItemList()
        {
            Tools.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
        }

        private void FillItemList()
        {
            var r = new Random();
            for (var i = 0; i < 3; i++)
            {
                Tools.CreateClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = r.Next(100, 10000) / 100m,
                    ProductId = Tools.ProviderAssociateProductId[i],
                    Quantity = (r.Next(0, 1000) % 10) + 1
                });
            }
        }

    }
}