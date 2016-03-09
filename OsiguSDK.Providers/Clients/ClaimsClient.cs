using System;
using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Providers.Models.Requests;
using RestSharp;
using OsiguSDK.Providers.Models;

namespace OsiguSDK.Providers.Clients
{
    public class ClaimsClient : BaseClient
    {
        public ClaimsClient(IConfiguration configuration) : base(configuration)
        {
        }

        /// <summary>
        /// Use this to create a new claim, this service will process asynchronously the claim information. The response of this service will tell to the client that the server has accepted the request but has not processed the coasurance and copayment value yet. This means that the client will have to poll to the service indicated at the location returned in http header.
        /// </summary>
        /// <param name="authorizationId">Osigu's authorization code</param>
        /// <param name="request">Clam request details</param>
        /// <returns>id of the queue resource for polling the claim creation status</returns>
        public String CreateClaim(string authorizationId, CreateClaimRequest request)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/authorizations/").Append(authorizationId).Append("/claims");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);

            var response = SendRequest(requestData);
            ValidateResponse(response);

            //after passing the validations return the url             
            var locationUrl = GetLocationHeader(response);

            //return only the id of the queue resource
            return GetIdFromResourceUrl(locationUrl);
        }

        public Claim ChangeClaimItems(string claimId, CreateClaimRequest request)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/claims/").Append(claimId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.PATCH, null, request);

            return ExecuteMethod<Claim>(requestData);
        }

        public Claim CompleteClaimTransaction(string claimId, Invoice request)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/claims/").Append(claimId).Append("/complete");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, "invoice", request);

            return ExecuteMethod<Claim>(requestData);
        }

        public Claim GetSingleClaim(string claimId)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/claims/").Append(claimId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null,null);

            return ExecuteMethod<Claim>(requestData);
        }

        public Pagination<Claim> GetListOfClaims(int? page = 0, int? size = 25)
        {
            var urlBuilder = new StringBuilder("/providers/").Append(Configuration.Slug).Append("/claims").Append("?page=").Append(page).Append("&size=").Append(size);
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, null);

            return ExecuteMethod<Pagination<Claim>>(requestData);
        }

    }
}
