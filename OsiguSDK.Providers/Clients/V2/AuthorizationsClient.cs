using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using RestSharp;

namespace OsiguSDK.Providers.Clients.v2
{
    public class AuthorizationsClient: BaseClient
    {
        public AuthorizationsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Models.v2.Authorization GetSingleAuthorization(string id)
        {
            var urlBuilder = new StringBuilder("/v2/providers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Models.v2.Authorization>(requestData);
        }
    }
}
