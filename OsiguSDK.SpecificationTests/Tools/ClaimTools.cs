using OsiguSDK.Core.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.Insurers.Models;
using Ploeh.AutoFixture;
using OsiguSDK.Insurers.Models.Requests;
using ServiceStack.Text;

namespace OsiguSDK.SpecificationTests.Tools
{

    public enum ClaimAmountRange
    {
        LESS_THAN_2800,
        BETWEEN_2800_AND_33600,
        GREATER_THAN_33600,
        EXACT_AMOUNT
    }

    public class ClaimTools
    {
        public IConfiguration ProviderBranchConfiguration { get; set; }
        public IConfiguration InsurerConfiguration { get; set; }
        private ClaimAmountRange ClaimAmountRange;
        private decimal ExactAmount;

        private decimal MinValue
        {
            get
            {
                switch (ClaimAmountRange)
                {
                    case ClaimAmountRange.LESS_THAN_2800:
                        return 1;
                    case ClaimAmountRange.BETWEEN_2800_AND_33600:
                        return 2801;
                    case ClaimAmountRange.GREATER_THAN_33600:
                        return 11200;
                }

                return 0;
            }
        }

        private decimal MaxValue
        {
            get
            {
                switch (ClaimAmountRange)
                {
                    case ClaimAmountRange.LESS_THAN_2800:
                        return 900;
                    case ClaimAmountRange.BETWEEN_2800_AND_33600:
                        return 10266;
                    case ClaimAmountRange.GREATER_THAN_33600:
                        return 300000;
                }

                return 0;
            }
        }

        private decimal Price
        {
            get
            {
                if (ClaimAmountRange == ClaimAmountRange.EXACT_AMOUNT)
                    return ExactAmount/3;

                var priceGenerator = new RandomGenerator();
                return priceGenerator.Next(MinValue, MaxValue) ;
            }
        }

        

        public ClaimTools()
        {
        }

        public ClaimTools(IConfiguration providerBranchConfiguration, IConfiguration insurerConfiguration)
        {
            ProviderBranchConfiguration = providerBranchConfiguration;
            InsurerConfiguration = insurerConfiguration;
        }

        public Providers.Models.Claim CreateRandomClaim(ClaimAmountRange amountRange = ClaimAmountRange.LESS_THAN_2800, decimal exactAmount = 0)
        {
            try
            {
                ClaimAmountRange = amountRange;
                ExactAmount = exactAmount;

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

                return Responses.Claim;


            }
            catch (Exception ex)
            {
                
                throw new Exception("An error occurred when creating a claim.  " + ex);
            }

        }

        public List<Providers.Models.Claim> CreateManyRandomClaims(int numberOfClaims, ClaimAmountRange amountRange = ClaimAmountRange.LESS_THAN_2800, decimal exactAmount = 0)
        {
            var claims = new List<Providers.Models.Claim>();

            for (int i = 0; i < numberOfClaims; i++)
            {
                var newClaim = CreateRandomClaim(amountRange, exactAmount);
                claims.Add(newClaim);
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
                    Price = Price,
                    ProductId = ConstantElements.ProviderAssociateProductId[i],
                    Quantity =  1m
                });
            }
        }
        
    }

    [TestFixture]
    public class ClaimToolsTests
    {
        
        [Test]
        public void CanCreateClaimWithExactAmount()
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            var claim = claimTools.CreateRandomClaim(ClaimAmountRange.EXACT_AMOUNT, 2800);

            claim.Should().NotBeNull();
            claim.Invoice.Amount.Should().Be(2800);

            Console.WriteLine(claim.Dump());
        }

        [Test]
        public void CanCreateClaimWithAmountLessThan2800()
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            var claim = claimTools.CreateRandomClaim();

            claim.Should().NotBeNull();
            claim.Invoice.Amount.Should().BeGreaterThan(0);
            claim.Invoice.Amount.Should().BeLessThan(2800);

            Console.WriteLine(claim.Dump());
        }

        [Test]
        public void CanCreateClaimWithAmountBetween2800And33600()
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            var claim = claimTools.CreateRandomClaim(ClaimAmountRange.BETWEEN_2800_AND_33600);

            claim.Should().NotBeNull();
            claim.Invoice.Amount.Should().BeGreaterThan(2800);
            claim.Invoice.Amount.Should().BeLessThan(33600);

            Console.WriteLine(claim.Dump());
        }

        [Test]
        public void CanCreateClaimWithAmountGreaterThan33600()
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            var claim = claimTools.CreateRandomClaim(ClaimAmountRange.GREATER_THAN_33600);

            claim.Should().NotBeNull();
            claim.Invoice.Amount.Should().BeGreaterThan(33600);
            claim.Invoice.Amount.Should().BeLessThan(900001);

            Console.WriteLine(claim.Dump());
        }


        [Test]
        public void CanCreateThreeClaimsWithAmountLessThan2800()
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            var claims = claimTools.CreateManyRandomClaims(3);

            claims.Should().NotBeNull();

            foreach (var claim in claims)
            {
                claim.Invoice.Amount.Should().BeGreaterThan(0);
                claim.Invoice.Amount.Should().BeLessThan(2800);
            }

            Console.WriteLine(claims.Dump());
        }

    }
}