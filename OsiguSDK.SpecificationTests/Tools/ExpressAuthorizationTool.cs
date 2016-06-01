using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using FizzWare.NBuilder;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools.TestingProducts;
namespace OsiguSDK.SpecificationTests.Tools
{
    public class ExpressAuthorizationTool
    {
        public IConfiguration Configuration { get; set; }

        private readonly ExpressAuthorizationClient _client;

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
                    return Math.Round(ExactAmount / 3, 2);

                var priceGenerator = new RandomGenerator();

                return Math.Round(priceGenerator.Next(MinValue, MaxValue), 2);
            }
        }

        public ExpressAuthorizationTool(IConfiguration configuration)
        {
            Configuration = configuration;

            _client = new ExpressAuthorizationClient(Configuration);
        }

        public string CreateExpressAuthorization(CreateExpressAuthorizationRequest request)
        {
            return _client.CreateExpressAuthorization(request);
            
        }

        public string CheckQueueStatus(string queueId)
        {
            var contSeconds = 0;
            const int timeOutLimit = 30;
            var stopwatch = new Stopwatch();

            var queueClient = new QueueClient(ConfigurationClients.ConfigProviderBranch1);
            var queueStatus = queueClient.CheckQueueStatus(queueId);
            
            stopwatch.Start();
            while (queueStatus.ResourceId == null && contSeconds <= timeOutLimit)
            {
                contSeconds++;
                Thread.Sleep(1000);
                queueStatus = queueClient.CheckQueueStatus(queueId);
            }
            stopwatch.Stop();

            if (queueStatus.ResourceId == null)
                throw new RequestException(
                    "The Timeout limit was exceeded when attempting get the express authorization with QueueId = " +
                    queueId + ". Timeout Limit setted = " + timeOutLimit + " seconds.");

            var expressAuthorization =
                TestClients.ExpressAuthorizationClient.GetSingleExpressAuthorization(queueStatus.ResourceId);

            Utils.Dump(
                "Time elapsed for getting the authorizationId(" + expressAuthorization.Id + "): {0:hh\\:mm\\:ss}",
                stopwatch.Elapsed);

            return queueStatus.ResourceId;
        }

        public List<AddOrModifyItemsExpressAuthorization.Item> GenerateProducts(int numberOfProduts, ClaimAmountRange amountRange = ClaimAmountRange.LESS_THAN_2800, decimal exactAmount = 0)
        {
            ClaimAmountRange = amountRange;
            ExactAmount = exactAmount;

            var productList = new List<AddOrModifyItemsExpressAuthorization.Item>();

            for (var i = 0; i < numberOfProduts; i++)
            {
                productList.Add(new AddOrModifyItemsExpressAuthorization.Item
                {
                    Price = Price,
                    ProductId = Provider1Products.ProviderExpressProductId[i],
                    Quantity = 1m
                });
            }

            return productList;
        }
    }

}