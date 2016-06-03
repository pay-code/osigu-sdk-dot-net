using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using FizzWare.NBuilder;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools.TestingProducts;
using Ploeh.AutoFixture;

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
                        return 100;
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
            while (queueStatus.ResourceId == null && contSeconds <= timeOutLimit && string.IsNullOrWhiteSpace(queueStatus.Error))
            {
                contSeconds++;
                Thread.Sleep(1000);
                queueStatus = queueClient.CheckQueueStatus(queueId);
            }
            stopwatch.Stop();
            Responses.QueueStatus = queueStatus;

            if (!string.IsNullOrEmpty(queueStatus.Error))
            {
                return string.Empty;
            }

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

        public List<AddOrModifyItemsExpressAuthorization.Item> CreateProductList(int numberOfProduts, ClaimAmountRange amountRange = ClaimAmountRange.LESS_THAN_2800, decimal exactAmount = 0)
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

        public ExpressAuthorization AddItemsOfAnExpressAuthorization(string authorizationId, List<AddOrModifyItemsExpressAuthorization.Item> items)
        {
            return _client.AddOrModifyItemsExpressAuthorization(authorizationId, new AddOrModifyItemsExpressAuthorization
            {
                Items = items
            });
        }

        public ExpressAuthorization CompleteExpressAuthorization(string expressAuthorizationId, Invoice invoice)
        {
            return _client.CompleteExpressAuthorization(expressAuthorizationId, new CompleteExpressAuthorizationRequest
            {
                Invoice = invoice
            });
        }

        public Invoice GenerateInvoice(decimal amount, string currency = "GTQ")
        {
            return TestClients.Fixture.Build<Invoice>()
                .With(x => x.Amount, amount)
                .With(x => x.Currency, currency)
                .With(x => x.DocumentDate, DateTime.UtcNow)
                .With(x => x.DigitalSignature, Guid.NewGuid().ToString())
                .Create();
        }


        public ExpressAuthorization CreateFullExpressAuthorization(CreateFullExpressAuthorizationRequest expressAuthorizationData)
        {
            ClaimAmountRange = expressAuthorizationData.Amount;
            ExactAmount = expressAuthorizationData.ExactAmount;

            var numberOfProducts = expressAuthorizationData.NumberOfProducts;
            var request = expressAuthorizationData.CreateExpressAuthorizationRequest;

            if (expressAuthorizationData.CreateExpressAuthorizationRequest == null)
            {
                request = new CreateExpressAuthorizationRequest
                {
                    InsurerId = ConstantElements.InsurerId.ToString(),
                    PolicyHolder = ConstantElements.PolicyHolder
                };
            }

            if (numberOfProducts == 0)
                numberOfProducts = 3;
            
            var queueId =_client.CreateExpressAuthorization(request);
            var authorizationId = CheckQueueStatus(queueId);
            var productList = CreateProductList(numberOfProducts, ClaimAmountRange);
            var expressAuthorization = AddItemsOfAnExpressAuthorization(authorizationId, productList);
            
            var amount = Math.Round(expressAuthorization.Items.Sum(x => x.Quantity * x.Price), 2);
            var coInsurancePercentage = Math.Round(expressAuthorization.Items.Average(x => x.CoInsurancePercentage) / 100,2);
            var coInsureanceAmount = amount * coInsurancePercentage;
            
            var invoiceAmount = amount - coInsureanceAmount - expressAuthorization.Copayment;
            var invoice = GenerateInvoice(invoiceAmount);

            expressAuthorization = CompleteExpressAuthorization(authorizationId, invoice);

            return expressAuthorization;
        }
    }

    public class CreateFullExpressAuthorizationRequest
    {
        public CreateExpressAuthorizationRequest CreateExpressAuthorizationRequest { get; set; }
        public int NumberOfProducts { get; set; }
        public ClaimAmountRange Amount { get; set; }
        public decimal ExactAmount { get; set; }
    }

}