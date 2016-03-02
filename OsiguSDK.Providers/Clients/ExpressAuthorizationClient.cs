using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using RestSharp;

namespace OsiguSDK.Providers.Clients
{
    public class ExpressAuthorizationClient : BaseClient
    {
        public ExpressAuthorizationClient(IConfiguration configuration) : base(configuration)
        {
        }

        public String CreateExpressAuthorization(CreateExpressAuthorizationRequest request)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/authorizations/express");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);

            var response = SendRequest(requestData);
            ValidateResponse(response);

            //after passing the validations return the url             
            var locationUrl = GetLocationHeader(response);

            //return only the id of the queue resource
            return GetIdFromResourceUrl(locationUrl);
        }

        public ExpressAuthorization AddOrModifyItemsExpressAuthorization(string expressAuthorizationId, AddOrModifyItemsExpressAuthorization request)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(expressAuthorizationId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);

            return ExecuteMethod<ExpressAuthorization>(requestData);
        }

        public ExpressAuthorization CompleteExpressAuthorization(string expressAuthorizationId, Invoice request)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(expressAuthorizationId).Append("/complete");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, "invoice", request);

            return ExecuteMethod<ExpressAuthorization>(requestData);
        }


        public void VoidExpressAuthorization(string expressAuthorizationId)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(expressAuthorizationId).Append("/void");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null,null);

            ExecuteMethod(requestData);
        }

        public ExpressAuthorization CompleteExpressAuthorization(string expressAuthorizationId)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(expressAuthorizationId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET,null,null);

            return ExecuteMethod<ExpressAuthorization>(requestData);
        }
    }
}
