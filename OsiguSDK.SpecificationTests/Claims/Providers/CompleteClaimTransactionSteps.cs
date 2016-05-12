using System;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CompleteClaimTransactionSteps
    {
        private static void CreateInvoice()
        {
            Responses.Invoice = new Invoice
            {
                Amount = Responses.Claim.Items.Sum(item => item.Price * item.Quantity) * 0.8m,
                Currency = "GTQ",
                DocumentDate = DateTime.Now,
                DocumentNumber = "12345"
            };
        }

        [When(@"I request the complete transaction request with an invalid claim id")]
        public void WhenIRequestTheCompleteTransactionRequestWithAnInvalidClaimId()
        {
            CreateInvoice();
            try
            {
                TestClients.ClaimsProviderClient.CompleteClaimTransaction("1234560", new CompleteClaimRequest
                {
                    Invoice = Responses.Invoice
                });
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

        [When(@"I request the complete transaction request")]
        public void WhenIRequestTheCompleteTransactionRequest()
        {
            CreateInvoice();

            try
            {
                TestClients.ClaimsProviderClient.CompleteClaimTransaction(Responses.Claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = Responses.Invoice
                });
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
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
                    Responses.Claim.Id = 0;
                    break;
                case "Invoice":
                    Responses.Invoice = null;
                    break;
                case "Amount":
                    Responses.Invoice.Amount = 0m;
                    break;
                case "Currency":
                    Responses.Invoice.Currency = string.Empty;
                    break;
                case "DocumentNumber":
                    Responses.Invoice.DocumentNumber = string.Empty;
                    break;
                default:
                    ScenarioContext.Current.Pending();
                    break;
            }

            try
            {
                TestClients.ClaimsProviderClient.CompleteClaimTransaction(Responses.Claim.Id.ToString(), new CompleteClaimRequest
                {
                    Invoice = Responses.Invoice
                });
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
        }

    }
}
