using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests.v1;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Osigu.Provider.Test
{
    [TestClass]
    public class QueueClientTest
    {
        [TestMethod]
        public void ShouldReturnCheckQueueStatusExpress()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var config = Configuration.LoadFromFile("provider-test.json");

            var policyHolder = new PolicyHolderInfo()
            {
                Id = "50258433",
                DateOfBirth = new DateTime(1967, 07, 20, 12, 01, 12).ToUniversalTime()
            };

            var request = fixture.Create<CreateExpressAuthorizationRequest>();
            request.PolicyHolder = policyHolder;
            request.InsurerId = "1";
            request.IllnessOnset = DateTime.Now;

            var expressClient = new ExpressAuthorizationClient(config);
            var expressAuth = new ExpressAuthorization();

            var queueExpress = new QueueClient(config);
            var queueStatus = new QueueStatus();
            var itemExpress = new AddOrModifyItemsExpressAuthorization.Item();
            var list = new List<AddOrModifyItemsExpressAuthorization.Item>();
            var addModifyItems = new AddOrModifyItemsExpressAuthorization();
            string id = expressClient.CreateExpressAuthorization(request);

                queueStatus = queueExpress.CheckQueueStatus(id);

            while (queueStatus.ResourceId == null)
            {
                queueStatus = queueExpress.CheckQueueStatus(id);
            }

                expressAuth = expressClient.GetSingleExpressAuthorization(queueStatus.ResourceId);

            Assert.IsInstanceOfType(expressAuth, typeof(ExpressAuthorization));
            Assert.IsNotNull(expressAuth);
            Assert.IsNotNull(queueStatus.ResourceId);
            Assert.AreEqual("COMPLETED", queueStatus.Status.ToString());
            
        }
    }
}
