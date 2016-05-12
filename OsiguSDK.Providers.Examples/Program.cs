using System.Collections.Generic;
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
            var testAuth = "123";
            var authResult = _client.Authorizations.GetSingleAuthorization(testAuth);
            

            //CREATING A CLAIM

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


            //GET THE STATUS OF THE QUEUE
            var status = _client.Queue.CheckQueueStatus(claimResult);

            while (string.IsNullOrEmpty(status.ResourceId))
            {
                status = _client.Queue.CheckQueueStatus(claimResult);
            }
            

            



        }
    }
}
