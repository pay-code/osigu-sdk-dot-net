using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using System;
using System.Collections.Generic;

namespace OsiguSDKExamples
{
    class AuthorizationExpressProviderExamples
    {
        private ExpressAuthorizationClient _client;
        public AuthorizationExpressProviderExamples(IConfiguration configuration)
        {
            _client = new ExpressAuthorizationClient(configuration);
        }

        public string CreateExpressAuthorization()
        {
            var request = new CreateExpressAuthorizationRequest
            {
                InsurerId = "1",
                PolicyHolder = new PolicyHolderInfo
                {
                    Id = "022165654654654",
                    DateOfBirth = DateTime.UtcNow.AddYears(-25)
                }
            };
            return _client.CreateExpressAuthorization(request);
        }

        public ExpressAuthorization AddOrModifyItemsExpressAuthorization(string expressAuthorizationId)
        {
            var request = new AddOrModifyItemsExpressAuthorization
            {
                Items = new List<AddOrModifyItemsExpressAuthorization.Item>
                {
                    new AddOrModifyItemsExpressAuthorization.Item
                    {
                        ProductId = "101",
                        Quantity = 1,
                        Price = 46.98m
                    },
                    new AddOrModifyItemsExpressAuthorization.Item
                    {
                        ProductId = "100",
                        Quantity = 5,
                        Price = 7.5m
                    },
                    new AddOrModifyItemsExpressAuthorization.Item
                    {
                        ProductId = "256",
                        Quantity = 1,
                        Price = 10.5m
                    }
                }
            };

            return _client.AddOrModifyItemsExpressAuthorization(expressAuthorizationId, request);
        }

        public ExpressAuthorization CompleteExpressAuthorization(string expressAuthorizationId, bool hasInvoice = true)
        {
            var request = new Invoice
            {
                DocumentNumber = "S-4 15654987987",
                DocumentDate = DateTime.UtcNow
            };
            return hasInvoice ? _client.CompleteExpressAuthorization(expressAuthorizationId, request) : _client.GetSingleExpressAuthorization(expressAuthorizationId);
        }

        public void VoidExpressAuthorization(string expressAuthorizationId)
        {
            _client.VoidExpressAuthorization(expressAuthorizationId);
        }
    }
}
