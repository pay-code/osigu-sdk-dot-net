using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models.V1;
using OsiguSDK.Insurers.Models.Requests;
using RestSharp;

namespace OsiguSDK.Insurers.Clients.V1
{
    public class ClaimsClient : BaseClient

    {
        public ClaimsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public Pagination<Claim> GetListOfClaims(string authorizationId, int? page = 0, int? size = 25)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(authorizationId).Append("/claims").Append("?page=").Append(page).Append("&size=").Append(size);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Pagination<Claim>>(requestData);
        }

        public Claim GetSingleClaim(string authorizationId, int claimId)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/").Append(authorizationId).Append("/claims/").Append(claimId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Claim>(requestData);
        }

        public void Approve(int id, ApproveClaimRequest date)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/claims/").Append(id).Append("/approve");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, date);
            
            ExecuteMethod(requestData);
        }

        public void Pay(int id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/claims/").Append(id).Append("/pay");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }

        public void Reject(int id, RejectRequest request)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/claims/").Append(id).Append("/reject");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);
            ExecuteMethod(requestData);
        }

        public void ReceiveDocumentation(int id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/claims/").Append(id).Append("/receive-documentation");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }

    }
}
