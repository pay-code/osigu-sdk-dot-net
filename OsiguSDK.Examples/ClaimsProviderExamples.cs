using System;
using System.Collections.Generic;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests.v1;

namespace OsiguSDKExamples
{
    class ClaimsProviderExamples
    {
        private ClaimsClient _client;
        public ClaimsProviderExamples(IConfiguration configuration)
        {
            _client = new ClaimsClient(configuration);
        }

        public string CreateClaim(string authorizationId)
        {
            var createRequest = InitializeRequest();

            return _client.CreateClaim(authorizationId, createRequest);
        }

        private static CreateClaimRequest InitializeRequest()
        {
            return new CreateClaimRequest
            {
                Pin = "123",
                Items = new List<CreateClaimRequest.Item>
                {
                    new CreateClaimRequest.Item
                    {
                        ProductId = "F101",
                        SubstituteProductId = "F201",
                        Price = 46.98m,
                        Quantity = 1
                    },
                    new CreateClaimRequest.Item
                    {
                        ProductId = "F100",
                        Quantity = 5,
                        Price = 7.5m
                    }
                }
            };
        }

        public Claim ChangeClaimItems(string claimId)
        {
            var request = InitializeRequest();
            request.Items.RemoveAll(x => x.Price > 0m);
            request.Items.Add(new CreateClaimRequest.Item
            {
                ProductId = "F101",
                SubstituteProductId = "F201",
                Price = 46.98m,
                Quantity = 2
            });

            request.Items.Add(new CreateClaimRequest.Item
            {
                ProductId = "F100",
                Price = 7.5m,
                Quantity = 2
            });

            return _client.ChangeClaimItems(claimId, request);
        }

        public Claim CompleteClaimTransaction(string claimId)
        {
            var request = new CompleteClaimRequest
            {
                Invoice = new Invoice
                { 
                    DocumentNumber = "S-4 15654987987",
                    DocumentDate = DateTime.UtcNow,
                    Currency = "USD",
                    Amount = 54.48m
                }
            };

            return _client.CompleteClaimTransaction(claimId, request);
        }

        public Claim GetSingleClaim(string claimId)
        {
            return _client.GetSingleClaim(claimId);
        }

        public Pagination<Claim> GetListOfClaims(int? page = 0, int? size = 25)
        {
            return _client.GetListOfClaims();
        }
      
    }
}