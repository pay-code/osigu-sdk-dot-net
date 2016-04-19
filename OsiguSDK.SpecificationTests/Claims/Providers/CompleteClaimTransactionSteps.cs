using System;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using TechTalk.SpecFlow;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CompleteClaimTransactionSteps
    {
        private static void CreateInvoice()
        {
            Tools.Invoice = new Invoice
            {
                Amount = Tools.Claim.Items.Sum(item => item.Price * item.Quantity),
                Currency = "GTQ",
                DocumentDate = DateTime.UtcNow,
                DocumentNumber = "12345"
            };
        }

        [When(@"I request the complete transaction request with an invalid claim id")]
        public void WhenIRequestTheCompleteTransactionRequestWithAnInvalidClaimId()
        {
            CreateInvoice();
            try
            {
                Tools.ClaimsProviderClient.CompleteClaimTransaction("1234560", new CompleteClaimRequest
                {
                    Invoice = Tools.Invoice
                });
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the complete transaction request")]
        public void WhenIRequestTheCompleteTransactionRequest()
        {
            CreateInvoice();
            try
            {
                Tools.ClaimsProviderClient.CompleteClaimTransaction(Tools.Claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = Tools.Invoice
                });
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the complete transaction request with missing fields")]
        public void WhenIRequestTheCompleteTransactionRequestWithMissingFields(Table scenario)
        {
            var scenarioValues = scenario.Rows.ToList().First();
            var missingField = scenarioValues["MissingField"];
            CreateInvoice();

            switch (missingField)
            {
                case "ClaimId":
                    Tools.Claim.Id = 0;
                    break;
                case "Invoice":
                    Tools.Invoice = null;
                    break;
                case "Amount":
                    Tools.Invoice.Amount = 0m;
                    break;
                case "Currency":
                    Tools.Invoice.Currency = string.Empty;
                    break;
                case "DocumentNumber":
                    Tools.Invoice.DocumentNumber = string.Empty;
                    break;
                default:
                    ScenarioContext.Current.Pending();
                    break;
            }

            try
            {
                Tools.ClaimsProviderClient.CompleteClaimTransaction(Tools.Claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = Tools.Invoice
                });
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
        }

    }
}
