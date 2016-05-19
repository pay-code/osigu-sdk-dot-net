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
using Ploeh.AutoFixture;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    [Binding]
    public class CreateMultipleClaimsInDifferentProvidersSteps
    {
        public static int CompletedClaims { get; set; }

        private static readonly Random Random = new Random(Guid.NewGuid().GetHashCode());
        private static void GenerateItemList()
        {
            Requests.CreateClaimRequest.Items = new List<CreateClaimRequest.Item>();
            FillItemList();
        }

        private static void FillItemList()
        {
            
            for (var i = 0; i < 3; i++)
            {
                Requests.CreateClaimRequest.Items.Add(new CreateClaimRequest.Item
                {
                    Price = Random.Next(500, 20000) / 100m,
                    ProductId = ConstantElements.ProviderAssociateProductId[i],
                    Quantity = (Random.Next(0, 1000) % 10) + 1
                });
            }
        }

        public static Insurers.Models.Authorization CreateValidAuthorizationRequest(int id)
        {
            TestClients.Fixture.Customizations.Add(new StringBuilder());
            Requests.SubmitAuthorizationRequest = TestClients.Fixture.Create<CreateAuthorizationRequest>();
            Requests.SubmitAuthorizationRequest.ReferenceId = Random.Next(10000, 1000000).ToString();
            Requests.SubmitAuthorizationRequest.ExpiresAt = Requests.SubmitAuthorizationRequest.AuthorizationDate.AddDays(1);
            Requests.SubmitAuthorizationRequest.Doctor.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.CountryCode = "GT";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Email = "mail@mail.com";
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.Id = ConstantElements.RPNTestPolicyNumber;
            Requests.SubmitAuthorizationRequest.Policy.PolicyHolder.DateOfBirth = ConstantElements.RPNTestPolicyBirthday;

            for (var pos = 0; pos < Requests.SubmitAuthorizationRequest.Items.Count; pos++)
            {
                Requests.SubmitAuthorizationRequest.Items[pos].ProductId = ConstantElements.InsurerAssociatedProductId[pos];
            }

            Insurers.Models.Authorization authorization;

            try
            {
                var responseAuthorization = TestClients.InsurerAuthorizationClient.CreateAuthorization(Requests.SubmitAuthorizationRequest);
                authorization = responseAuthorization;
            }
            catch (RequestException exception)
            {
                Console.WriteLine("error in claim {0}", id);
                Console.WriteLine(exception.Message);
                CompletedClaims++;
                throw;
            }

            return authorization;
        }

        private static string CreateClaim(int id, Insurers.Models.Authorization authorization)
        {
            authorization.Pin.Should().NotBeNullOrEmpty("The authorization was not compleated correctly");
            Requests.CreateClaimRequest = TestClients.Fixture.Create<CreateClaimRequest>();
            Requests.CreateClaimRequest.Pin = authorization.Pin;
            var queue = string.Empty;
            GenerateItemList();
            try
            {
                queue = TestClients.ClaimsProviderClient.CreateClaim(authorization.Id, Requests.CreateClaimRequest);
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
            var i = 0;
            for (i = 0; i < 500; i++)
            {
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
            if (i == 500)
            {
                throw new Exception("Reached max amount of tries");
            }

            return queueStatus;
        }

        private static Claim GetClaim(int id, QueueStatus queueStatus)
        {
            Claim claim = null;
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

        private static void CreateInvoice(Claim claim)
        {
            Responses.Invoice = new Invoice
            {
                Amount = claim.Items.Sum(item => item.Price * item.Quantity) * 0.8m,
                Currency = "GTQ",
                DocumentDate = DateTime.UtcNow,
                DocumentNumber = "12345"
            };
        }

        private static void CompleteClaim(int id, Claim claim)
        {
            CreateInvoice(claim);
            CompletedClaims++;
            try
            {
                TestClients.ClaimsProviderClient.CompleteClaimTransaction(claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = Responses.Invoice
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
            Console.WriteLine("Creating Authorization...");
            var authorization = CreateValidAuthorizationRequest(id);
            Console.WriteLine("Creating Claim...");
            var queueId = CreateClaim(id, authorization);
            Console.WriteLine("Waiting for Status...");
            var queueStatus = GetClaimStatus(id, queueId);
            Console.WriteLine("Getting Claim...");
            var claim = GetClaim(id, queueStatus);
            Console.WriteLine("Completing Claim...");
            CompleteClaim(id, claim);
        }

        private static IDictionary<string,string> ScenarioValues { get; set; }
        [Given(@"I have the provider selected claims client")]
        public void GivenIHaveTheProviderSelectedClaimsClient(Table scenario)
        {
            ScenarioValues = scenario.Rows.First();
            Responses.ErrorId = 0;
            TestClients.ClaimsProviderClient = new ClaimsClient(ConfigurationClients.ConfigProviderBranch1);
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
                Thread.Sleep(100);
            }

            for (var i = 0; i < times * 20 && CompletedClaims < times; i++)
            {
                Thread.Sleep(500);
            }
        }
    }
}
