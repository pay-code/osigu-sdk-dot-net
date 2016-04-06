using System;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using TechTalk.SpecFlow;
using OsiguSDK.Providers.Models;

namespace OsiguSDK.SpecificationTests.Claims.Providers
{
    [Binding]
    public class CompleteClaimTransactionSteps
    {
        private static Invoice CreateInvoice()
        {
            return new Invoice
            {
                Amount = Tools.Claim.Items.Sum(item => item.Price * item.Quantity),
                Currency = "GTQ",
                DocumentDate = DateTime.UtcNow,
                DocumentNumber = "12345"
            };
        }

        [When(@"I request the complete transaction request")]
        public void WhenIRequestTheCompleteTransactionRequest()
        {
            try
            {
                Tools.ClaimsProviderClient.CompleteClaimTransaction(Tools.Claim.Id.ToString(), CreateInvoice());
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

            switch (missingField)
            {
                case "ClaimId":
                    break;
                //case ""
            }
        }

    }
}
