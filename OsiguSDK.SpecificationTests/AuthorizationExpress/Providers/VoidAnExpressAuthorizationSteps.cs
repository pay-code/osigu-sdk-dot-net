using System;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class VoidAnExpressAuthorizationSteps
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }
        public ExpressAuthorizationHelper ExpressAuthorizationHelper { get; set; }

        public VoidAnExpressAuthorizationSteps()
        {
            ExpressAuthorizationHelper = new ExpressAuthorizationHelper();
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"The authorization status is different to pending review or null")]
        public void GivenTheAuthorizationStatusIsDifferentToPendingReviewOrNull()
        {
            
        }


        [Given(@"I have entered a invalid authorization id")]
        public void GivenIHaveEnteredAInvalidAuthorizationId()
        {
            Responses.ExpressAuthorizationId = "xxxx";
        }

        [Given(@"I have completed the express authorizaion")]
        public void GivenIHaveCompletedTheExpressAuthorizaion()
        {
            var invoice = ExpressAuthorizationTool.GenerateInvoice(
                Math.Round(CurrentData.ExpressAutorizationItems.Sum(x => x.Quantity*x.Price), 2));

            ExpressAuthorizationHelper.CompleteExpressAuthorization(Responses.ExpressAuthorizationId,invoice);
        }


        [When(@"I make the void express authorization request to the endpoint")]
        public void WhenIMakeTheVoidExpressAuthorizationRequestToTheEndpoint()
        {
            Responses.ErrorId = 204;
            try
            {
                TestClients.ExpressAuthorizationClient.VoidExpressAuthorization(Responses.ExpressAuthorizationId);

                Utils.Dump("ExpressAuthorization Response: ", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("AddOrModifyItemsExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }
        }
        
        [Then(@"the result should be no content")]
        public void ThenTheResultShouldBeNoContent()
        {
            Responses.ErrorId.Should().Be(204);
        }
    }
}
