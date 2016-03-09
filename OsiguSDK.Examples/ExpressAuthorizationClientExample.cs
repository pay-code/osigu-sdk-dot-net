using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Clients;

namespace OsiguSDKExamples
{
    class ExpressAuthorizationClientExample
    {
        private ExpressAuthorizationClient _client;

        public ExpressAuthorizationClientExample(IConfiguration configuration)
        {
            _client = new ExpressAuthorizationClient(configuration);
        }

        public ExpressAuthorization GetSingleAuthorization(string id)
        {
            return _client.GetSingleAuthorization(id);
        }

        public Pagination<ExpressAuthorization> GetListOfAuthorizationExpress(ExpressAuthorizationClient.ExpressAuthorizationStatus status = ExpressAuthorizationClient.ExpressAuthorizationStatus.INSURER_PENDING_REVIEW, int? page = 0, int? size = 25)
        {
            return _client.GetListOfAuthorizationsExpress(status, page, size);
        }

        public void ApproveExpressAuthorization(string id)
        {
            _client.ApproveExpressAuthorization(id);
        }

        public void RejectExpressAuthorization(string id)
        {
            _client.RejectExpressAuthorization(id);
        }
    }
}
