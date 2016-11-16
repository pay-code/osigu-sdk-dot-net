using System;
using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models.Requests.v1;
using RestSharp;

namespace OsiguSDK.Providers.Clients.v2
{
    public class ExpressAuthorizationClient : BaseClient
    {
        public ExpressAuthorizationClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Models.v2.ExpressAuthorization AddOrModifyItemsExpressAuthorization(string expressAuthorizationId, AddOrModifyItemsExpressAuthorization request)
        {
            var urlBuilder = new StringBuilder("/v2/providers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(expressAuthorizationId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.PATCH, null, request);

            return ExecuteMethod<Models.v2.ExpressAuthorization>(requestData);
        }
    }
}
