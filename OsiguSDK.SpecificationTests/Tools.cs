using System;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Products.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Claim = OsiguSDK.Providers.Models.Claim;
using SubmitProductRequest = OsiguSDK.Providers.Models.Requests.SubmitProductRequest;

namespace OsiguSDK.SpecificationTests
{
    public static class Tools
    {
        public static string BaseUrl
        {
            get { return @"http://10.0.1.13:5000"; }
        }

        public static readonly Fixture Fixture = new Fixture();
        public static ProductsClient ProductsProviderClient { get; set; }
        public static ProductsClient ProductsProductsClientWithNoPermission { get; set; }
        public static SubmitProductRequest SubmitProductRequest { get; set; }

        public static ClaimsClient ClaimsProviderClient { get; set; }
        public static ClaimsClient ClaimsProviderClientWithNoPermission { get; set; }
        public static CreateClaimRequest CreateClaimRequest { get; set; }
        public static Claim Claim { get; set; }
        public static Invoice Invoice { get; set; }

        public static QueueClient QueueProviderClient { get; set; }
        public static QueueClient QueueProviderClientWithNoPermission { get; set; }
        public static QueueStatus QueueStatus { get; set; }
        public static string QueueId { get; set; }

        public static AuthorizationsClient providerAuthorizationClient { get; set; }

        public static Insurers.Clients.ProductsClient productsInsurerClient { get; set; }
        public static Insurers.Models.Requests.SubmitProductRequest submitInsurerProductRequest { get; set; }

        public static Insurers.Clients.AuthorizationsClient insurerAuthorizationClient { get; set; }
        public static CreateAuthorizationRequest submitAuthorizationRequest { get; set; }

        public static RestClient RestClient { get; set; }

        public static OsiguProductRequest OsiguProductRequest { get; set; }
        
        public static Settlement settlementRequest { get; set; }

        public static int ErrorId { get; set; }
        public static int ErrorId2 { get; set; }
        public static RequestException RequestException { get; set; }


        //public static readonly string[] InsurerAssociateProductId = { "QAINSURER1" , "QAINSURER2", "QAINSURER3" } ; DEV
        public static readonly string[] InsurerAssociateProductId = { "QAINSURER1_TEST", "QAINSURER2_TEST", "QAINSURER3_TEST" } ;
        //public static readonly string[] ProviderAssociateProductId = { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" }; //DEV
        public static readonly string[] ProviderAssociateProductId = { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" };
        //public static readonly string[] OsiguProductId = { "1016241", "1016242", "1016243" }; //DEV
        public static readonly string[] OsiguProductId = { "18551", "18552", "18553" };

        public static readonly string RPNTestPolicyNumber = "50258433";
        public static readonly DateTime RPNTestPolicyBirthday = new DateTime(1967, 7, 20, 7, 0, 0, 123);

        public static string AuthorizationId { get; set; }
        public static string PIN { get; set; }


        public class StringBuilder : ISpecimenBuilder
        {
            private readonly Random rnd = new Random();

            public object Create(object request, ISpecimenContext context)
            {
                var type = request as Type;

                if (type == null || type != typeof(string))
                {
                    return new NoSpecimen();
                }

                return rnd.Next(0, 1000000).ToString();
            }
        }

        public class WebClient
        {
            public static string Get(string urlPath)
            {
                string responseString;
                var url = BaseUrl + urlPath;
                using (var client = new System.Net.WebClient())
                {
                    responseString = client.DownloadString(url);
                }

                return responseString;
            }

           
        }

       
    }
}
