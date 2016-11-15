using System;
using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using RestSharp;

namespace OsiguSDK.Providers.Clients.V2
{
    public class ExpressAuthorizationClient : BaseClient
    {
        public ExpressAuthorizationClient(IConfiguration configuration) : base(configuration)
        {
        }

        public ExpressAuthorizationV2 AddOrModifyItemsExpressAuthorization(string expressAuthorizationId, AddOrModifyItemsExpressAuthorization request)
        {
            var urlBuilder = new StringBuilder("/v2/providers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(expressAuthorizationId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.PATCH, null, request);

            return ExecuteMethod<ExpressAuthorizationV2>(requestData);
        }
    }
}
