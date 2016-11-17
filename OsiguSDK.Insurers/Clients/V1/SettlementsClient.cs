using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using RestSharp;


namespace OsiguSDK.Insurers.Clients.v1
{
    public class SettlementsClient: BaseClient
    {
        public SettlementsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public void SendConsolidatedReport(string date_to)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/settlement/send-consolidated-report").Append("?date_to=").Append(date_to);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }

    }
}
