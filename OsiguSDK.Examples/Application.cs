using System;
using System.Linq;
using AutoMapper;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Insurers.Models.Requests;
using IConfiguration = OsiguSDK.Core.Config.IConfiguration;
using OsiguSDK.Insurers.Models.v1;
using OsiguSDK.Insurers.Clients.v1;
using OsiguSDK.Insurers.Models.Requests.v1;

namespace OsiguSDKExamples
{
    static class Application
    {
        static void Main(string[] args)
        {
            IConfiguration configInsurer = new Configuration()
            {
                BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                Slug = "test-insurer",
                Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
            };

            IConfiguration configProvider = new Configuration()
            {
                BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                Slug = "test-provider",
                Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
            };
//
//            var configInsurer = Configuration.LoadFromFile("insurer-test.json");
//            var configProvider = Configuration.LoadFromFile("provider-test.json");

            //automapper configs
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Authorization, CreateAuthorizationRequest>();
                cfg.CreateMap<Authorization.Item, CreateAuthorizationRequest.Item>();
            }).CreateMapper();

            ////////////////////////////////////////////
            // INSURERS
            ////////////////////////////////////////////
            //Authorization examples
            var authorizationResponse = AuthorizationsInsurerExamples(configInsurer, mapper);

            ClaimsInsurerExamples(configInsurer, authorizationResponse);

            ProductsInsurerExamples(configInsurer);

            //ExpressAuthorizationInsurerExamples(configInsurer);

            ////////////////////////////////////////////
            // Providers
            ////////////////////////////////////////////
            //var authorizationProviderId = AuthorizationProviderExamples(configProvider);
            /*
            InsurersProviderExamples(configProvider);

            ClaimsProviderExamples(configProvider, authorizationProviderId);

            ProductsProviderExample(configProvider);
            */
            //ExpressAuthorizationsProviderExamples(configProvider);
        }

        private static void ExpressAuthorizationsProviderExamples(IConfiguration configProvider)
        {
            var expressAuthProviderExamples = new AuthorizationExpressProviderExamples(configProvider);
            var queueProviderExamples = new QueueProviderExample(configProvider);

            var createAuthExpressResponse = expressAuthProviderExamples.CreateExpressAuthorization();
            var checkExpressAuthorizationResponse = queueProviderExamples.CheckQueueStatus("8975261");

            var modifyItemsAuthExpressResponse = expressAuthProviderExamples.AddOrModifyItemsExpressAuthorization("EXP-GT-123545");
            var completeAuthExpressResponse = expressAuthProviderExamples.CompleteExpressAuthorization("EXP-GT-123545", true);
            completeAuthExpressResponse = expressAuthProviderExamples.CompleteExpressAuthorization("EXP-GT-123545");

            //var getAuthExpressResponse = expressAuthProviderExamples.
            expressAuthProviderExamples.VoidExpressAuthorization("EXP-GT-123545");
        }

        private static void ProductsProviderExample(IConfiguration configProvider)
        {
            var productsProviderExamples = new ProductsProviderExamples(configProvider);
            productsProviderExamples.SubmitProduct();
            productsProviderExamples.SubmitRemoval("M215");
        }

        private static void ClaimsProviderExamples(IConfiguration configProvider, string authorizationProviderId)
        {
            var claimsProviderExamples = new ClaimsProviderExamples(configProvider);
            var queueProviderExamples = new QueueProviderExample(configProvider);
            var createClaimResponse = claimsProviderExamples.CreateClaim(authorizationProviderId);
            var checkClaimStatusResponse = queueProviderExamples.CheckQueueStatus(createClaimResponse);

            var getClaimResponse = claimsProviderExamples.GetSingleClaim("121556");
            var getListOfClaimsResponse = claimsProviderExamples.GetListOfClaims();
            var changeClaimItemsResponse = claimsProviderExamples.ChangeClaimItems("125666554");
            var completedClaimResponse = claimsProviderExamples.CompleteClaimTransaction("121556");

        }

        private static void InsurersProviderExamples(IConfiguration configProvider)
        {
            var insurersExamples = new InsurersProviderExample(configProvider);

            var insurersResponse = insurersExamples.GetInsurers();
        }

        /*private static string AuthorizationProviderExamples(IConfiguration config)
        {
            var authorizationProvidersExamples = new AuthorizationsProviderExample(config);
            //CREATE AUTHORIZATION
            var authorizationProviderResponse = authorizationProvidersExamples.GetSingleAuthorization("1234566554");

            //return authorizationProviderResponse.Id;
        }

*/        private static void ExpressAuthorizationInsurerExamples(IConfiguration config)
        {
            var expressAuthExamples = new ExpressAuthorizationClientExample(config);

            //GET AN EXPRESS AUTHORIZATION
            var getSingleAuthorizationResponse = expressAuthExamples.GetSingleAuthorization("EXP-GT-12345");

            //GET LIST OF AUTHORIZATION EXPRESS
            var getExpressAuthorizationListResponse = expressAuthExamples.GetListOfAuthorizationExpress(ExpressAuthorizationsClient.ExpressAuthorizationStatus.INSURER_PENDING_REVIEW);

            //APPROVE AN EXPRESS AUTHORIZATION
            //expressAuthExamples.ApproveExpressAuthorization("EXP-GT-12345");

            //REJECT AN EXPRESS AUTHORIZATION
            //expressAuthExamples.RejectExpressAuthorization("EXP-GT-12345");
        }

        private static void ClaimsInsurerExamples(IConfiguration config, Authorization authorizationResponse)
        {
            var claimExamples = new ClaimsExamples(config);

            //GET ALL THE CLAIMS OF AN AUTHORIZATION
            var claimList = claimExamples.GetListOfClaims(authorizationResponse.Id);

            //GET A SINGLE CLAIM
            if (claimList.Content.ToList().Count > 0)
            {
                var claim = claimExamples.GetSingleClaim(authorizationResponse.Id, claimList.Content.First().Id);
            }
        }

        private static Authorization AuthorizationsInsurerExamples(IConfiguration config, IMapper mapper)
        {
            var authorizationExamples = new AuthorizationsExamples(config);

            //CREATE AUTHORIZATION
            var authorizationResponse = authorizationExamples.CreateAuthorization();

            
            //MODIFY THE AUTHORIZATION
            authorizationResponse.AuthorizationDate = DateTime.UtcNow.AddDays(-1);
            var modificationResponse = authorizationExamples.ModifyTheAuthorization(authorizationResponse.Id, mapper.Map<CreateAuthorizationRequest>(authorizationResponse));

            //GET THE AUTHORIZATION
            var getAuthorizationResponse = authorizationExamples.GetRecentlyCreatedAuthorization(authorizationResponse.Id);

            //VOID THE AUTHORIZATION
            authorizationExamples.VoidTheAuthorization(modificationResponse.Id);
            return authorizationResponse;
        }

        private static void ProductsInsurerExamples(IConfiguration config)
        {
            var productExamples = new ProductsExample(config);

            // SUBMIT A PRODUCT
            productExamples.SubmitProduct();

            // GET LIST OF PRODUCTS
            var getListProductsResponse = productExamples.GetListOfProducts();

            // GET A SINGLE PRODUTC
            var getProductResponse = productExamples.GetSingleProduct(getListProductsResponse.Content.First().ProductId);

            
        }
    }
}

