using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients.v1;
using OsiguSDK.Insurers.Clients.v1;
using OsiguSDK.Insurers.Models.v1;
using OsiguSDK.Insurers.Models.v1;

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
            return _client.GetList(authorizationId);
        }

        public Claim GetSingleClaim(string authorizationId, int claimId)
        {
            return _client.GetSingle(authorizationId, claimId);
        }

    }
}
