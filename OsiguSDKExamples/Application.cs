using System;
using System.Linq;
using AutoMapper;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using IConfiguration = OsiguSDK.Core.Config.IConfiguration;

namespace OsiguSDKExamples
{
    class Application
    {
        static void Main(string[] args)
        {
            IConfiguration config = new Configuration(){
                BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                Slug = "test-insurer",
                Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")                
            };

            //automapper configs
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Authorization, CreateAuthorizationRequest>();
                cfg.CreateMap<Authorization.Item, CreateAuthorizationRequest.Item>()
;            }).CreateMapper();


            //////////////////////////////////////////////////
            // AUTHORIZATION EXAMPLES
            //////////////////////////////////////////////////            
            var authorizationExamples = new AuthorizationsExamples(config);

            //CREATE AUTHORIZATION
            var authorizationResponse = authorizationExamples.CreateAuthorization();

            //MODIFY THE AUTHORIZATION
            authorizationResponse.AuthorizationDate = DateTime.Now.AddDays(-1);
            var modificationResponse = authorizationExamples.ModifyTheAuthorization(authorizationResponse.Id, mapper.Map<CreateAuthorizationRequest>(authorizationResponse));


            //VOID THE AUTHORIZATION
            authorizationExamples.VoidTheAuthorization(modificationResponse.Id);



            //////////////////////////////////////////////////
            // CLAIM EXAMPLES
            //////////////////////////////////////////////////
            var claimExamples = new ClaimsExamples(config);

            //GET ALL THE CLAIMS OF AN AUTHORIZATION
            var claimList =  claimExamples.GetListOfClaims(authorizationResponse.Id);


            //GET A SINGLE CLAIM
            var claim = claimExamples.GetSingleClaim(authorizationResponse.Id, claimList.Content.First().Id);

        }
    }
}

