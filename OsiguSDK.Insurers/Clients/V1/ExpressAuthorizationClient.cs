using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using RestSharp;
using OsiguSDK.Insurers.Models.V1;

namespace OsiguSDK.Insurers.Clients.V1
{
    public class ExpressAuthorizationClient : BaseClient
    {

        public enum ExpressAuthorizationStatus
        {
            INSURER_PENDING_REVIEW,
            INSURER_APPROVED,
            INSURER_REJECTED,
            PENDING_PAID,
            PAID
        }

        public ExpressAuthorizationClient(IConfiguration configuration) : base(configuration)
        {
        }

        public ExpressAuthorization GetSingleAuthorization(string id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(id);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<ExpressAuthorization>(requestData);
        }

        public Pagination<ExpressAuthorization> GetListOfAuthorizationsExpress(ExpressAuthorizationStatus status = ExpressAuthorizationStatus.INSURER_PENDING_REVIEW,  int? page = 0, int? size = 25)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express").Append("?status=").Append(status.ToString().ToLower()).Append("&page=").Append(page).Append("&size=").Append(size); ;
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Pagination<ExpressAuthorization>>(requestData);
        }

        public void Review(string id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(id).Append("/review");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }

        public void Approve(string id, ApproveClaimRequest date)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(id).Append("/approve");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, date);
            ExecuteMethod(requestData);
        }

        public void Reject(string id, RejectRequest request)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(id).Append("/reject");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);
            ExecuteMethod(requestData);
        }

        public void Pay(string id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(id).Append("/pay");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }

        public void ReceiveDocumentation(string id)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/authorizations/express/").Append(id).Append("/receive-documentation");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);
            ExecuteMethod(requestData);
        }
    }
}
