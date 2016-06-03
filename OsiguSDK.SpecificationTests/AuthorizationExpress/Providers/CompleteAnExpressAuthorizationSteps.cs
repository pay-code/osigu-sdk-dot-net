using System;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;
using Ploeh.AutoFixture;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class CompleteAnExpressAuthorizationSteps
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }
        public ExpressAuthorizationHelper ExpressAuthorizationHelper { get; set; }

        public CompleteAnExpressAuthorizationSteps()
        {
            ExpressAuthorizationHelper = new ExpressAuthorizationHelper();
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have created a valid express authorization")]
        public void GivenIHaveCreatedAValidExpressAuthorization()
        {
            Responses.QueueId = ExpressAuthorizationHelper.CreateExpressAuthorization();
            Responses.ExpressAuthorizationId = ExpressAuthorizationHelper.CheckExpressAuthorizationStatus(Responses.QueueId);
            Responses.ExpressAuthorization = ExpressAuthorizationHelper.AddItemsOfAnExpressAuthorization(Responses.ExpressAuthorizationId, 2);
        }


        [Given(@"I have the request data for complete an express authorization")]
        public void GivenIHaveTheRequestDataForCompleteAnExpressAuthorization()
        {
            Requests.CompleteExpressAuthorizationRequest = new CompleteExpressAuthorizationRequest
            {
                Invoice = ExpressAuthorizationTool.GenerateInvoice(
                    Math.Round(CurrentData.ExpressAutorizationItems.Sum(x => x.Quantity*x.Price), 2))
            };
        }
        
        [Given(@"I have entered an invalid authorization id")]
        public void GivenIHaveEnteredAnInvalidAuthorizationId()
        {
            Responses.ExpressAuthorizationId = "xxxxxx";
        }
        
        [Given(@"The amount of invoice is negative")]
        public void GivenTheAmountOfInvoiceIsNegative()
        {
            Requests.CompleteExpressAuthorizationRequest.Invoice.Amount = -10;
        }
        
        [Given(@"The amount of invoice is equal to cero")]
        public void GivenTheAmountOfInvoiceIsEqualToCero()
        {
            Requests.CompleteExpressAuthorizationRequest.Invoice.Amount = 0;
        }
        
        [Given(@"The amount of invoice is greater than sum of products")]
        public void GivenTheAmountOfInvoiceIsGreaterThanSumOfProducts()
        {
            Requests.CompleteExpressAuthorizationRequest.Invoice.Amount += 1000;

        }
        
        [Given(@"The amount of invoice is less than sum of products")]
        public void GivenTheAmountOfInvoiceIsLessThanSumOfProducts()
        {
            Requests.CompleteExpressAuthorizationRequest.Invoice.Amount -= 30;
        }
        
        [Given(@"The required fields are missing")]
        public void GivenTheRequiredFieldsAreMissing()
        {
            Requests.CompleteExpressAuthorizationRequest = new CompleteExpressAuthorizationRequest
            {
                Invoice = new Invoice()
            };
        }
        
        [When(@"I make the complete express authorization request to the endpoint")]
        public void WhenIMakeTheCompleteExpressAuthorizationRequestToTheEndpoint()
        {
            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    TestClients.ExpressAuthorizationClient.CompleteExpressAuthorization(Responses.ExpressAuthorizationId, Requests.CompleteExpressAuthorizationRequest);

                Utils.Dump("ExpressAuthorization Response: ", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("AddOrModifyItemsExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }
        }
        
        [Then(@"the result should be the express authorization including the invoice sent")]
        public void ThenTheResultShouldBeTheExpressAuthorizationIncludingTheInvoiceSent()
        {
            
        }
    }
}
