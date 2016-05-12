using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;

namespace OsiguSDK.Providers.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var _config = Configuration.LoadFromFile("provider-test.json");
            var _client = new Client(_config);


            //GET A SINGLE PRE-AUTHORIZATION
            //http://docs.paycode.apiary.io/#reference/providers/authorization/getting-an-authorization
            var testAuth = "123";
            var authResult = _client.Authorizations.GetSingleAuthorization(testAuth);


            //CREATING A CLAIM
            //http://docs.paycode.apiary.io/#reference/providers/claims/creating-a-claim
            var claim = new CreateClaimRequest()
            {
                Pin = "123",
                Items = new List<CreateClaimRequest.Item>()
                {
                    new CreateClaimRequest.Item()
                    {
                        ProductId = authResult.Items[0].ProductId,
                        Quantity = authResult.Items[0].Quantity,
                        Price = 100
                    }
                }
            };
            
            var claimResult = _client.Claims.CreateClaim(testAuth,claim);


            //CHECK THE CLAIMS STATUS
            //http://docs.paycode.apiary.io/#reference/providers/claims/check-claim-status

            var queueStatus = _client.Queue.CheckQueueStatus(claimResult);
            
            while (string.IsNullOrEmpty(queueStatus.ResourceId))
            {
                queueStatus = _client.Queue.CheckQueueStatus(claimResult);
            }


            //COMPLETE THE TRANSACTION
            //http://docs.paycode.apiary.io/#reference/providers/claims/completing-the-claim-transaction
            var completeRequest = new CompleteClaimRequest()
            {
                Invoice = new Invoice()
                {
                    DocumentNumber = "A-1 3423443",
                    DocumentDate = DateTime.Now,
                    Currency = "GTQ",
                    Amount = claim.Items.Sum(x => x.Quantity*x.Price)
                }
            };

            var completeRequestResult = _client.Claims.CompleteClaimTransaction(queueStatus.ResourceId, completeRequest);

            Console.WriteLine("PRE-AUTHORIZATION COMPLETE");
            Console.WriteLine(completeRequestResult.Id);

        }
    }
}
