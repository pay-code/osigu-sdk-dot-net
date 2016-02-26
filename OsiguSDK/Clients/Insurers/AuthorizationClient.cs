using System;
using System.Net;
using System.Text;
using OsiguSDK.Config;
using OsiguSDK.Infra;
using OsiguSDK.Models.Insurers.Requests;
using OsiguSDK.Models.Insurers.Responses;
using RestSharp;

namespace OsiguSDK.Clients.Insurers
{
    public class AuthorizationClient : BaseClient
    {

        public AuthorizationClient(IConfiguration configuration) : base(configuration)
        {
        }

        public AuthorizationResponse CreateAuthorization(AuthorizationRequest request)
        {            
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);           
            return ExecuteMethod<AuthorizationResponse>(requestData);
        }

        public AuthorizationResponse GetAuthorization(string id)
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
