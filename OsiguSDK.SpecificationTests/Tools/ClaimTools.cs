using OsiguSDK.Core.Config;
using System;
using System.Collections.Generic;
using System.Threading;
using FizzWare.NBuilder;
using FluentAssertions;
using NUnit.Framework;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using Ploeh.AutoFixture;
using OsiguSDK.Insurers.Models.Requests;
using ServiceStack.Text;
using System.Linq;
using OsiguSDK.Providers.Models;
using System.Diagnostics;


namespace OsiguSDK.SpecificationTests.Tools
{

   

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
                    case ClaimAmountRange.EXACT_AMOUNT:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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
                    case ClaimAmountRange.EXACT_AMOUNT:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return 0;
            }
        }

        private decimal Price
        {
            get
            {
                if (ClaimAmountRange == ClaimAmountRange.EXACT_AMOUNT)
                    return Math.Round(ExactAmount /3,2);

                var priceGenerator = new RandomGenerator();

                //TODO: Remove Round
                return Math.Round(priceGenerator.Next(MinValue, MaxValue), 2);
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

        public Claim CreateRandomClaim(ClaimAmountRange amountRange = ClaimAmountRange.LESS_THAN_2800, decimal exactAmount = 0)
        {
            try
            {
                ClaimAmountRange = amountRange;
                ExactAmount = exactAmount;

                Responses.ErrorId = 0;

                TestClients.ClaimsProviderClient = new ClaimsClient(ProviderBranchConfiguration);
                Console.WriteLine("Claims provider client passed");

                TestClients.InsurerAuthorizationClient = new Insurers.Clients.AuthorizationsClient(InsurerConfiguration);
                Console.WriteLine("Insurer authorization client passed");

                TestClients.QueueProviderClient = new QueueClient(ProviderBranchConfiguration);
                Console.WriteLine("Queue provider client passed");

                SetAuthorizationRequestData();
                Console.WriteLine("Set authorization request passed");

                DoAuthotizationPost();
                Console.WriteLine("Do authotization post passed");

                SetClaimRequestData();
                Console.WriteLine("Set claim request data passed");

                Responses.QueueId = TestClients.ClaimsProviderClient.CreateClaim(Responses.Authorization.Id, Requests.CreateClaimRequest);
                Console.WriteLine("QueueId generated = " + Responses.QueueId);

                Responses.Claim = GetClaim();
                Console.WriteLine("ClaimId generated = " + Responses.Claim.Id);

                CreateInvoice();
                Console.WriteLine("Invoice was generated");

                TestClients.ClaimsProviderClient.CompleteClaimTransaction(Responses.Claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = Responses.Invoice
                });

                Responses.Claim.Invoice = Responses.Invoice;
                Console.WriteLine("Claim was returned");
                return Responses.Claim;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred when creating a claim.  " + ex);
            }
        }

        private static Claim GetClaim()
        {
            var contSeconds = 0;
            const int timeOutLimit = 30;

            var stopwatch = new Stopwatch();

            Responses.QueueStatus = TestClients.QueueProviderClient.CheckQueueStatus(Responses.QueueId);
            Responses.QueueStatus.ResourceId = null;

            stopwatch.Start();
            while (Responses.QueueStatus.ResourceId == null || contSeconds > timeOutLimit)
            {
                contSeconds++;
                Thread.Sleep(1000);
                Responses.QueueStatus = TestClients.QueueProviderClient.CheckQueueStatus(Responses.QueueId);
            }
            stopwatch.Stop();

            if (Responses.QueueStatus == null)
                throw new Exception("The Timeout limit was exceeded when attempting get the claim with QueueId = " + Responses.QueueId + ". Timeout Limit setted = " + timeOutLimit + " seconds.");

            var claim = TestClients.ClaimsProviderClient.GetSingleClaim(Responses.QueueStatus.ResourceId);

            Console.WriteLine("Time elapsed for getting the claimId(" + claim.Id + "): {0:hh\\:mm\\:ss}", stopwatch.Elapsed);

            return claim;

        }

        public List<Claim> CreateManyRandomClaims(int numberOfClaims, ClaimAmountRange amountRange = ClaimAmountRange.LESS_THAN_2800, decimal exactAmount = 0)
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
            Responses.Authorization = new Insurers.Models.Authorization {Id = "1"};
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
                    Price = Price, ProductId = ConstantElements.ProviderAssociateProductId[i], Quantity = 1m
                });
            }
        }

        private void CreateInvoice()
        {
            Responses.Invoice = new Invoice
            {
                Amount = Math.Round(Responses.Claim.Items.Sum(item => item.Price*item.Quantity) * 0.8m, 2),
                Currency = "GTQ",
                DocumentDate = DateTime.UtcNow,
                DocumentNumber = "12345"
            };
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
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1, ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
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
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1, ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
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
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1, ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
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
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1, ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
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
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1, ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
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