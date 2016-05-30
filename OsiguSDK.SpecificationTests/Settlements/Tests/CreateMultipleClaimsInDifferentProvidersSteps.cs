using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.Providers.Models;
using System.Threading;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using OsiguSDK.SpecificationTests.Tools.TestingProducts;
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;
using ServiceStack.Text;

namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    [Binding]
    public class CreateMultipleClaimsInDifferentProvidersSteps
    {
        public static int CompletedClaims { get; set; }

        private static Random _random;

        public CreateMultipleClaimsInDifferentProvidersSteps()
        {
            _random = new Random(Guid.NewGuid().GetHashCode());
        }

        public static Insurers.Models.Authorization CreateValidAuthorizationRequest(int id)
        {
            TestClients.Fixture.Customizations.Add(new StringBuilder());
            var submitAuthorizationRequest = TestClients.Fixture.Create<CreateAuthorizationRequest>();
            submitAuthorizationRequest.ReferenceId = _random.Next(10000, 1000000).ToString();
            submitAuthorizationRequest.ExpiresAt = submitAuthorizationRequest.AuthorizationDate.AddDays(1);
            submitAuthorizationRequest.Doctor.CountryCode = "GT";
            submitAuthorizationRequest.Policy.CountryCode = "GT";
            submitAuthorizationRequest.Policy.InsuranceCompanyCode = "50";
            submitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            submitAuthorizationRequest.Policy.PolicyHolder.Id = ConstantElements.RPNTestPolicyNumber;
            submitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = ConstantElements.RPNTestPolicyBirthday;

            var provider = int.Parse(ScenarioValues["Provider"]);
            for (var pos = 0; pos < submitAuthorizationRequest.Items.Count; pos++)
            {
                submitAuthorizationRequest.Items[pos].ProductId = provider == 1
                    ? Provider1Products.InsurerAssociatedProductId[pos]
                    : provider == 2
                        ? Provider2Products.InsurerAssociatedProductId[pos]
                        : Provider3Products.InsurerAssociatedProductId[pos];
            }

            Insurers.Models.Authorization authorization = null;

            try
            {
                authorization = TestClients.InsurerAuthorizationClient.CreateAuthorization(submitAuthorizationRequest);
            }
            catch (RequestException exception)
            {
                Console.WriteLine("error in claim {0}", id);
                Console.WriteLine(exception.Message);
                submitAuthorizationRequest.Dump();
                CompletedClaims++;
                throw;
            }

            return authorization;
        }

        private static string CreateClaim(int id, Insurers.Models.Authorization authorization)
        {
            authorization.Pin.Should().NotBeNullOrEmpty("The authorization was not compleated correctly");
            var createClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            createClaimRequest.Pin = authorization.Pin;
            string queue;

            createClaimRequest.Items = new List<CreateClaimRequest.Item>();
            var provider = int.Parse(ScenarioValues["Provider"]);
            for (var i = 0; i < 3; i++)
            {
                createClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = _random.Next(500, 20000)/100m,
                    ProductId =
                        provider == 1
                            ? Provider1Products.ProviderAssociateProductId[i]
                            : provider == 2
                                ? Provider2Products.ProviderAssociateProductId[i]
                                : Provider3Products.ProviderAssociateProductId[i],
                    Quantity = authorization.Items[i].Quantity
                });
            }

            try
            {
                queue = TestClients.ClaimsProviderClient.CreateClaim(authorization.Id, createClaimRequest);
            }
            catch (RequestException exception)
            {
                Console.WriteLine("error in claim {0}", id);
                Console.WriteLine(exception.Message);
                CompletedClaims++;
                throw;
            }

            return queue;
        }

        private static QueueStatus GetClaimStatus(int id, string queueId)
        {
            QueueStatus queueStatus = null;
            int i;
            for (i = 0; i < 200; i++)
            {
                if (i%25 == 0)
                {
                    Console.WriteLine("Consulting Claim id {0}... current tries {1}, last response: {2}: {3}", id, i,
                        queueStatus?.Status, queueStatus?.Error);
                }
                try
                {
                    queueStatus = TestClients.QueueProviderClient.CheckQueueStatus(queueId);
                }
                catch
                {
                    // ignored
                }

                if (!string.IsNullOrEmpty(queueStatus?.ResourceId))
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            if (i != 200) return queueStatus;
            CompletedClaims++;
            throw new Exception("Reached max amount of tries");
        }

        private static Claim GetClaim(int id, QueueStatus queueStatus)
        {
            Claim claim;
            try
            {
                claim = TestClients.ClaimsProviderClient.GetSingleClaim(queueStatus.ResourceId);
            }
            catch (RequestException exception)
            {
                Console.WriteLine("error in claim {0}", id);
                Console.WriteLine(exception.Message);
                CompletedClaims++;
                throw;
            }

            return claim;
        }

        private static Invoice CreateInvoice(Claim claim)
        {
            return new Invoice
            {
                Amount = claim.Items.Sum(item => item.Price*item.Quantity) - claim.TotalCoInsurance - claim.Copayment,
                Currency = "GTQ",
                DocumentDate = DateTime.UtcNow,
                DocumentNumber = "12345"
            };
        }

        private static void CompleteClaim(int id, Claim claim)
        {
            CompletedClaims++;
            try
            {
                TestClients.ClaimsProviderClient.CompleteClaimTransaction(claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = CreateInvoice(claim)
                });
                Console.WriteLine("Completed successfully claim {0}", id);
            }
            catch (RequestException exception)
            {
                Console.WriteLine("Error in claim {0}", id);
                Console.WriteLine(exception.Message);
            }

            Console.WriteLine("Remaining claims {0}",
                (int.Parse(ScenarioValues["ClaimQuantity"]) - CompletedClaims));
        }

        private static void CompleteClaimProcess(int id)
        {
            Console.WriteLine("Creating authorization {0}", id);
            var authorization = CreateValidAuthorizationRequest(id);

            Console.WriteLine("Creating claim {0}", id);
            var queueId = CreateClaim(id, authorization);

            Console.WriteLine("Consulting claim {0}", id);
            var queueStatus = GetClaimStatus(id, queueId);

            Console.WriteLine("Getting claim {0}", id);
            var claim = GetClaim(id, queueStatus);

            Console.WriteLine("Completing claim {0}", id);
            CompleteClaim(id, claim);
        }

        private static IDictionary<string, string> ScenarioValues { get; set; }

        [Given(@"I have the provider selected claims client")]
        public void GivenIHaveTheProviderSelectedClaimsClient(Table scenario)
        {
            ScenarioValues = scenario.Rows.First();
            Responses.ErrorId = 0;
            var provider = int.Parse(ScenarioValues["Provider"]);
            TestClients.ClaimsProviderClient =
                new ClaimsClient(provider == 1
                    ? ConfigurationClients.ConfigProviderBranch1
                    : provider == 2
                        ? ConfigurationClients.ConfigProviderBranch2
                        : ConfigurationClients.ConfigProviderBranch3);
        }

        [When(@"I complete the claim creation process")]
        public void WhenICompleteTheClaimCreationProcess()
        {
            var times = int.Parse(ScenarioValues["ClaimQuantity"]);
            CompletedClaims = 0;
            for (var i = 0; i < times; i++)
            {
                var id = i;
                var thread = new Thread(() => CompleteClaimProcess(id));
                thread.Start();
                Thread.Sleep(5000);
            }

            for (var i = 0; i < times*50 && CompletedClaims < times; i++)
            {
                Thread.Sleep(1000);
            }
        }
    }
}