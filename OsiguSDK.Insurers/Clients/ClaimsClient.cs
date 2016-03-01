using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models;
using RestSharp;

namespace OsiguSDK.Insurers.Clients
{
    public class ClaimsClient : BaseClient

    {
        public ClaimsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Pagination<Claim> GetListOfClaims(string authorizationId, int? page = 0, int? size = 25)
        {
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(authorizationId).Append("/claims").Append("?page=").Append(page).Append("&size=").Append(size);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Pagination<Claim>>(requestData);
        }

        public Claim GetSingleClaim(string authorizationId, int claimId)
        {
            var urlBuilder = new StringBuilder("/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(authorizationId).Append("/claims/").Append(claimId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Claim>(requestData);
        }
    }
}
