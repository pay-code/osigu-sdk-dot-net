using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Models.Requests.v1;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;

namespace Osigu.Provider.Test
{
    [TestClass]
    public class ClaimsClientTest
    {

        public string resourseID;

        [TestMethod]
        public void ShouldCreateClaim()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var claimsClient = new ClaimsClient(config);
            var itemList = new List<CreateClaimRequest.Item>();
            var createClaimRequest = new CreateClaimRequest();

            var item = new CreateClaimRequest.Item()
            {
                Quantity = 1,
                Price = 100,
                OsiguProductId = "14851",
                ProductId = "111601"
            };

            createClaimRequest.Pin = "8718";
            itemList.Add(item);
            createClaimRequest.Items = itemList;

            string location = claimsClient.CreateClaim("JG2SJYAP5", createClaimRequest);

            var queueClient = new QueueClient(config);
            var queueStatus = new QueueStatus();
                queueStatus = queueClient.CheckQueueStatus(location);

            while (queueStatus.ResourceId == null)
            {
                queueStatus = queueClient.CheckQueueStatus(location);
            }
            resourseID = queueStatus.ResourceId;
            Assert.IsNotNull(queueStatus.ResourceId);
            Assert.AreEqual("COMPLETED", queueStatus.Status.ToString());
            
        }
        
        [TestMethod]
        public void ShouldChangeClaimItems()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var claimsClient = new ClaimsClient(config);
            var claim = new Claim();
            var createClaimRequest = new CreateClaimRequest(); 
            var listItem = new List<CreateClaimRequest.Item>();
            var item = new CreateClaimRequest.Item()
            {
                Quantity = 3,
                Price = 100,
                OsiguProductId = "",
                ProductId = "",
                SubstituteProductId = "146638",
            };

            listItem.Add(item);
            createClaimRequest.Items = listItem;
            createClaimRequest.Pin = "7915";
            claim = claimsClient.ChangeClaimItems("199478", createClaimRequest);

            Assert.IsNotNull(claim);
            Assert.IsInstanceOfType(claim, typeof(Claim));
        }


        
        [TestMethod]
        public void ShouldCompleteClaimTransaction()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var claimsClient = new ClaimsClient(config);
            var itemList = new List<CreateClaimRequest.Item>();
            var createClaimRequest = new CreateClaimRequest();
            var item = new CreateClaimRequest.Item()
            {
                Quantity = 3,
                Price = 100,
                OsiguProductId = "2372",
                ProductId = "009897"
            };

            var completeClaimRequest = new CompleteClaimRequest()
            {
                Invoice = new Invoice()
                {
                    Amount = 100,
                    Currency = "Q"
                }
            };

            createClaimRequest.Pin = "7915";
            itemList.Add(item);
            createClaimRequest.Items = itemList;

            string location = claimsClient.CreateClaim("JFA8LQIA1", createClaimRequest);

            var queueClient = new QueueClient(config);
            var queueStatus = new QueueStatus();
            queueStatus = queueClient.CheckQueueStatus(location);

            while (queueStatus.ResourceId == null)
            {
                queueStatus = queueClient.CheckQueueStatus(location);
            }

            Assert.IsNotNull(queueStatus.ResourceId);
            Assert.AreEqual("COMPLETED", queueStatus.Status.ToString());
        }
    }
}
