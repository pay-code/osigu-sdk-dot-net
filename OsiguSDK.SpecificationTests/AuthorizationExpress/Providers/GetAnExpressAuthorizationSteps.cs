using System;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Tools;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    [Binding]
    public class GetAnExpressAuthorizationSteps
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }
        public ExpressAuthorizationHelper ExpressAuthorizationHelper { get; set; }

        public GetAnExpressAuthorizationSteps()
        {
            ExpressAuthorizationHelper = new ExpressAuthorizationHelper();
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
        }

        [Given(@"I have created an complete express authorization")]
        public void GivenIHaveCreatedAnCompleteExpressAuthorization()
        {
            Responses.ExpressAuthorization =
                ExpressAuthorizationTool.CreateFullExpressAuthorization(new CreateFullExpressAuthorizationRequest());

            Responses.ExpressAuthorizationId = Responses.ExpressAuthorization.Id;
        }

        [When(@"I make the get express authorization request to the endpoint")]
        public void WhenIMakeTheGetExpressAuthorizationRequestToTheEndpoint()
        {
            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    TestClients.ExpressAuthorizationClient.GetSingleExpressAuthorization(Responses.ExpressAuthorizationId);

                Utils.Dump("ExpressAuthorization Response: ", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("TestClients.ExpressAuthorizationClient.GetSingleExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }
        }
        
        [Then(@"the authorization express data should be the expected")]
        public void ThenTheAuthorizationExpressDataShouldBeTheExpected()
        {
            ExpressAuthorizationHelper.ValidateExpressAuthorizationResponse();    
        }
    }
}
