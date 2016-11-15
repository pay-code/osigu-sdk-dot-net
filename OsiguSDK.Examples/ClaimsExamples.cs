using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Clients.V1;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.V1;

namespace OsiguSDKExamples
{
    public class ClaimsExamples
    {
        private readonly ClaimsClient _client;
        public ClaimsExamples(IConfiguration config)
        {
            _client = new ClaimsClient(config);
        }

        public Pagination<Claim> GetListOfClaims(string authorizationId)
        {
            return _client.GetListOfClaims(authorizationId);
        }

        public Claim GetSingleClaim(string authorizationId, int claimId)
        {
            return _client.GetSingleClaim(authorizationId, claimId);
        }

    }
}
