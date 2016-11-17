using System.Text;
using Newtonsoft.Json;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Insurers.Models.Requests.v1;
using RestSharp;
using OsiguSDK.Insurers.Models.v1;

namespace OsiguSDK.Insurers.Clients.v1
{
    public class AuthorizationsClient : BaseClient
    {

        public AuthorizationsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Authorization Create(CreateAuthorizationRequest request)
        {            
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations");
            var s = JsonConvert.SerializeObject(request);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);           
            return ExecuteMethod<Authorization>(requestData);
        }

        public Authorization GetSingle(string id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Authorization>(requestData);
        }

        public Authorization Modify(string id, CreateAuthorizationRequest request)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.PUT, null, request);
            return ExecuteMethod<Authorization>(requestData);            
        }

        public void Void(string id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id).Append("/void");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }
    }
}
