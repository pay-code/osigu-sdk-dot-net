﻿using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models;
using RestSharp;

namespace OsiguSDK.Providers.Clients.v1
{
    public class AuthorizationsClient: BaseClient
    {
        public AuthorizationsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Authorization GetSingleAuthorization(string id)
        {
            var urlBuilder = new StringBuilder("/v1/providers/").Append(Configuration.Slug).Append("/authorizations/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Authorization>(requestData);
        }
    }
}
