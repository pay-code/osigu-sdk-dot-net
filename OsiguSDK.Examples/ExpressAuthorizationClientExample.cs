using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients.v1;

using OsiguSDK.Insurers.Models.v1;

namespace OsiguSDKExamples
{
    class ExpressAuthorizationClientExample
    {
        private ExpressAuthorizationsClient _client;

        public ExpressAuthorizationClientExample(IConfiguration configuration)
        {
            _client = new ExpressAuthorizationsClient(configuration);
        }

        public ExpressAuthorization GetSingleAuthorization(string id)
        {
            return _client.GetSingle(id);
        }

        public Pagination<ExpressAuthorization> GetListOfAuthorizationExpress(ExpressAuthorizationsClient.ExpressAuthorizationStatus status = ExpressAuthorizationsClient.ExpressAuthorizationStatus.INSURER_PENDING_REVIEW, int? page = 0, int? size = 25)
        {
            return _client.GetList(status, page, size);
        }


        //public void ApproveExpressAuthorization(string id)
        //{
        //_client.ApproveExpressAuthorization(id);
        //}

        //public void RejectExpressAuthorization(string id)
        //{
        //_client.RejectExpressAuthorization(id);
        //}

    }
}
