using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Insurers.Models.Responses;
using RestSharp;

namespace OsiguSDK.Insurers.Clients
{
    public class AuthorizationsClient : BaseClient
    {

        public AuthorizationsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public AuthorizationResponse CreateAuthorization(AuthorizationRequest request)
        {            
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);           
            return ExecuteMethod<AuthorizationResponse>(requestData);
        }

        public AuthorizationResponse GetSingleAuthorization(string id)
        {
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<AuthorizationResponse>(requestData);
        }

        public AuthorizationResponse ModifyAuthorization(string id, AuthorizationRequest request)
        {
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.PUT, null, null);
            return ExecuteMethod<AuthorizationResponse>(requestData);            
        }

        public void VoidAuthorization(string id)
        {
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id).Append("/void");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }
    }
}
