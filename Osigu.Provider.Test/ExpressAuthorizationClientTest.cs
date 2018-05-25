using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Models.Requests.v1;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Osigu.Provider.Test
{
    [TestClass]
    public class ExpressAuthorizationClientTest
    {
        [TestMethod]
        public void ShouldCreateAnExpressAuthorization()
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

            var express = new ExpressAuthorizationClient(config);

            string id = express.CreateExpressAuthorization(request);

            Assert.IsNotNull(id);
        }

        [TestMethod]
        public void ShouldGetSingleExpressAuthorization()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var express = new ExpressAuthorizationClient(config);
            var authorization = new ExpressAuthorization();
                authorization = express.GetSingleExpressAuthorization("EXP-JDLW1GH2");

            Assert.IsInstanceOfType(authorization, typeof(ExpressAuthorization));
        }

        

        [TestMethod]
        public void ShouldAddOrModifyItemsExpressAuthorization()
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

                itemExpress.Quantity = 3;
                itemExpress.Price = 100;
                itemExpress.ProductId = "132124";

                list.Add(itemExpress);

                addModifyItems.Items = list;
                expressAuth = expressClient.AddOrModifyItemsExpressAuthorization(queueStatus.ResourceId, addModifyItems);

            Assert.IsInstanceOfType(expressAuth, typeof(ExpressAuthorization));
            Assert.IsNotNull(expressAuth.Items);

        }

        [TestMethod]
        public void ShouldCompleteExpressAuthorization()
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

                itemExpress.Quantity = 3;
                itemExpress.Price = 100;
                itemExpress.ProductId = "132124";

                list.Add(itemExpress);
                addModifyItems.Items = list;

            expressAuth = expressClient.AddOrModifyItemsExpressAuthorization(queueStatus.ResourceId, addModifyItems);

            var completeAuthRequest = new CompleteExpressAuthorizationRequest();

                    completeAuthRequest.Invoice = new Invoice();
                    completeAuthRequest.Invoice.DocumentNumber = "DM 2018032704132125";
                    completeAuthRequest.Invoice.DocumentDate = DateTime.UtcNow;
                    completeAuthRequest.Invoice.Amount = 130;
                    completeAuthRequest.Invoice.Currency = "GTQ";

                expressAuth = expressClient.CompleteExpressAuthorization(queueStatus.ResourceId, completeAuthRequest);

            Assert.IsInstanceOfType(expressAuth, typeof(ExpressAuthorization));
            Assert.IsNotNull(expressAuth);
            Assert.IsNotNull(expressAuth.InvoiceDetails);
        }

        [TestMethod]
        public void ShouldVoidExpressAuthorization()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var express = new ExpressAuthorizationClient(config);
            var authorization = new ExpressAuthorization();
                authorization = express.GetSingleExpressAuthorization("EXP-JF9YK9HV");

            Assert.IsInstanceOfType(authorization, typeof(ExpressAuthorization));
        }
    }
}
