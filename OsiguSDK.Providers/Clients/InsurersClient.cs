using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models;
using RestSharp;

namespace OsiguSDK.Providers.Clients
{
    public class InsurersClient : BaseClient
    {
        public InsurersClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Pagination<Insurer> GetInsurers()
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/insurers");
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Pagination<Insurer>>(requestData);
        }
    }
}
