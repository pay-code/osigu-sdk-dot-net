using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;

namespace OsiguSDKExamples
{
    class AuthorizationsProviderExample
    {
        private AuthorizationsClient _client;
        public AuthorizationsProviderExample(IConfiguration configuration)
        {
            _client = new AuthorizationsClient(configuration);
        }

        public Authorization GetSingleAuthorization(string id)
        {
            return _client.GetSingleAuthorization(id);
        }
    }
}
