using System;
using System.Collections.Generic;
using System.Linq;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using Ploeh.AutoFixture;
using RestSharp;
using RestSharp.Extensions.MonoHttp;
using ServiceStack.Text;
using Claim = OsiguSDK.Providers.Models.Claim;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class SettlementTool
    {
        public static SettlementResponse GetLastSettlement()
        {
            if (TestClients.InternalRestClient == null)
                TestClients.InternalRestClient = new InternalRestClient(ConfigurationClients.ConfigSettlement);

            var page = "0";
            var size = "100";
            var pagination = $"?page={page}&size={size}";
            var response = TestClients.InternalRestClient.RequestToEndpoint<Pagination<SettlementResponse>>(Method.GET, pagination);

            if (!response.LastPage)
            {
                page =  HttpUtility.ParseQueryString(response.Links.Last.Href).Get("page");
                size = HttpUtility.ParseQueryString(response.Links.Last.Href).Get("size");
                pagination = $"?page={page}&size={size}";
                response = TestClients.InternalRestClient.RequestToEndpoint<Pagination<SettlementResponse>>(Method.GET, pagination);
            }


            if(!response.Content.Any())
                throw new Exception("Settlements not found");

            var settlements = response.Content.ToList();
            
            return settlements.First(x => x.Id == settlements.Max(y => y.Id));

        }

        public static SettlementResponse CreateSettlement(SettlementType settlementType, int numberOfClaims,
            ClaimAmountRange claimsAmountRange)
        {
            if (TestClients.InternalRestClient == null)
                TestClients.InternalRestClient = new InternalRestClient(ConfigurationClients.ConfigSettlement);

            CurrentData.Claims = CreateClaims(numberOfClaims, claimsAmountRange);
            CreateSettlementRequest();

            try
            {
                var settlementCreated = TestClients.InternalRestClient.RequestToEndpoint<SettlementResponse>(Method.POST, "/" + settlementType.ToString().ToLower(), Requests.SettlementRequest);
                Responses.ErrorId = 201;

                return settlementCreated;

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
                Responses.ResponseMessage = exception.Message;
                Responses.Errors = exception.Errors;
            }

            return new SettlementResponse();
        }
        
        private static void CreateSettlementRequest()
        {
            Requests.ProviderId = ConstantElements.RetentionProviderId;
            Requests.InsurerId = ConstantElements.InsurerId;
            Requests.InitialDate = DateTime.Now.AddMinutes(-1);
            Requests.EndDate = DateTime.Now;

            var claimsIds = CurrentData.Claims.Select(claim => new SettlementItemRequest
            {
                ClaimId = claim.Id.ToString()
            }).ToList();

            Requests.SettlementRequest = TestClients.Fixture.Build<SettlementRequest>()
                .With(x => x.From, Requests.InitialDate)
                .With(x => x.To, Requests.EndDate)
                .With(x => x.ProviderId, Requests.ProviderId.ToString())
                .With(x => x.InsurerId, Requests.InsurerId.ToString())
                .With(x => x.SettlementsItems, claimsIds)
                .Create();


            Console.WriteLine("Settlement request " + Requests.SettlementRequest.Dump());
        }

        private static List<Claim> CreateClaims(int numberOfClaims, ClaimAmountRange claimsAmountRange)
        {
            var claimTools = new ClaimTools
            {
                InsurerConfiguration = ConfigurationClients.ConfigInsurer1,
                ProviderBranchConfiguration = ConfigurationClients.ConfigProviderBranch1
            };

            return claimTools.CreateManyRandomClaims(numberOfClaims, claimsAmountRange);
        }
    }
}