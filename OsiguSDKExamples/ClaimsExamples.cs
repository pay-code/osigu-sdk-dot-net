using System;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models.Responses;

namespace OsiguSDKExamples
{
    public class ClaimsExamples
    {
        private readonly ClaimsClient _client;
        public ClaimsExamples(IConfiguration config)
        {
            _client = new ClaimsClient(config);
        }

        public Pagination<ClaimResponse> GetListOfClaims(string authorizationId)
        {
            return _client.GetListOfClaims(authorizationId);
        }

        public ClaimResponse GetSingleClaim(string authorizationId, int claimId)
        {
            return _client.GetSingleClaim(authorizationId, claimId);
        }

    }
}
