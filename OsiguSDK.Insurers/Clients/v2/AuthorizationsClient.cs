using System;
using System.Text;
using Newtonsoft.Json;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Insurers.Models.Requests.v1;
using RestSharp;
using OsiguSDK.Insurers.Models.v1;
using OsiguSDK.Insurers.Models;

namespace OsiguSDK.Insurers.Clients.v2
{
    public class AuthorizationsClient : BaseClient
    {

        public AuthorizationsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Authorization Create(CreateAuthorizationRequest request)
        {            
            var urlBuilder = new StringBuilder("/v2/insurers/").Append(Configuration.Slug).Append("/authorizations");
            var s = JsonConvert.SerializeObject(request);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);           
            return ExecuteMethod<Authorization>(requestData);
        }

        public Authorization GetSingle(string id)
        {
            var urlBuilder = new StringBuilder("/v2/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Authorization>(requestData);
        }

        public Authorization Modify(string id, CreateAuthorizationRequest request)
        {
//            if   (request.Policy.GetType() != typeof(Models.v2.Policy) )
//            {
//                throw new Exception("only v2 is allowed here");
//            }
            
            var urlBuilder = new StringBuilder("/v2/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.PUT, null, request);
            return ExecuteMethod<Authorization>(requestData);
        }
        
    }
}
