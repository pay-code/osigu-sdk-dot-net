using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;

namespace OsiguSDKExamples
{
    class InsurersProviderExample
    {
        private InsurersClient _client;
        public InsurersProviderExample(IConfiguration config)
        {
            _client = new InsurersClient(config);
        }

        public Pagination<Insurer> GetInsurers()
        {
            return _client.GetInsurers();
        }
    }
}
