using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    public class ExpressAuthorizationHelper
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }

        public ExpressAuthorizationHelper()
        {
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
        }
        public string CreateExpressAuthorization()
        {
            string queuiId = null;
            var request = new CreateExpressAuthorizationRequest
            {
                InsurerId = ConstantElements.InsurerId.ToString(),
                PolicyHolder = ConstantElements.PolicyHolder
            };

            Utils.Dump("CreateExpressAuthorizationRequest: ", request);

            try
            {
                queuiId = ExpressAuthorizationTool.CreateExpressAuthorization(request);
                Utils.Dump("QueueId: ", Responses.QueueId);

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            return queuiId;
        }

        public string CheckExpressAuthorizationStatus(string queueId)
        {
            Responses.ErrorId = 0;
            string expressAuthorizationId = null;
            try
            {
                expressAuthorizationId = ExpressAuthorizationTool.CheckQueueStatus(queueId);

                Utils.Dump("CheckExpressAuthorizationStatus: ", expressAuthorizationId);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            return expressAuthorizationId;
        }

        public ExpressAuthorization AddItemsOfAnExpressAuthorization(string authorizationExpressId, int numberOfProducts)
        {
            CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(numberOfProducts);

            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    ExpressAuthorizationTool.AddItemsOfAnExpressAuthorization(authorizationExpressId,
                        CurrentData.ExpressAutorizationItems);

                Utils.Dump("ExpressAuthorization Response: ", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("AddOrModifyItemsExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }

            return Responses.ExpressAuthorization;

        }

        public ExpressAuthorization CompleteExpressAuthorization(string authorizationExpressId, Invoice invoice)
        {
            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    ExpressAuthorizationTool.CompleteExpressAuthorization(Responses.ExpressAuthorizationId, invoice);

                Utils.Dump("AuthorizationId: " + authorizationExpressId + " Completed.", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("CompleteExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }

            return Responses.ExpressAuthorization;
        }
    }
}