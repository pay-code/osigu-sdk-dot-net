﻿using System.Text;
using Newtonsoft.Json;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using RestSharp;


namespace OsiguSDK.Insurers.Clients
{
    public class SettlementsClient: BaseClient
    {
        public SettlementsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public void SendConsolidatedReport(string date_to)
        {
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/settlement/send-consolidated-report").Append("?date_to=").Append(date_to);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }

    }
}
