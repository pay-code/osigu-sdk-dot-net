using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using RestSharp;

namespace OsiguSDK.Providers.Clients.v1
{
    public class DiagnosesClient : BaseClient
    {
        public DiagnosesClient(IConfiguration configuration) : base(configuration)
        {
        }


        public Product GetDiagnosis(string diagnosisId)
        {
            var urlBuilder = new StringBuilder("/v1/diagnoses/").Append(diagnosisId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Product>(requestData);
        }


        public Pagination<Diagnosis> GetListOfDiagnoses()
        {
            var urlBuilder = new StringBuilder("/v1/diagnoses");
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Pagination<Diagnosis>>(requestData);
        }
    }
}
