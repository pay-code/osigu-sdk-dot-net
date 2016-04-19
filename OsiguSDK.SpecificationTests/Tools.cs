using System;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.Providers.Models;
using OsiguSDK.SpecificationTests.Products.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Claim = OsiguSDK.Providers.Models.Claim;


namespace OsiguSDK.SpecificationTests
{
    public static class Tools
    {
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
        public static Insurers.Models.Requests.CreateAuthorizationRequest submitAuthorizationRequest { get; set; }

        public static RestClient RestClient { get; set; }

        public static OsiguProductRequest OsiguProductRequest { get; set; }
        
        public static Settlement settlementRequest { get; set; }

        public static int ErrorId { get; set; }
        public static int ErrorId2 { get; set; }
        public static RequestException RequestException { get; set; }


        public static readonly string[] InsurerAssociateProductId = { "QAINSURER1" , "QAINSURER2", "QAINSURER3" } ;
        public static readonly string[] ProviderAssociateProductId = { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" };
        public static readonly string[] OsiguProductId = { "1016241", "1016242", "1016243" };

        public static readonly string RPNTestPolicyNumber = "50258433";
        public static readonly DateTime RPNTestPolicyBirthday = new DateTime(1967, 7, 20, 7, 0, 0, 123);

        public static string AuthorizationId { get; set; }
        public static string PIN { get; set; }

        public static IConfiguration ConfigInsurersSandbox { get
        {
            return _configInsurersSandbox ?? (_configInsurersSandbox = new Configuration
            {
                BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                Slug = "test-insurer",
                Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
            });
        } }
        private static IConfiguration _configInsurersSandbox;

        public static IConfiguration ConfigProvidersSandbox { get
        {
            return _configProvidersSandbox ?? (_configProvidersSandbox = new Configuration
            {
                BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                Slug = "test-insurer",
                Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
            });
        } }

        private static IConfiguration _configProvidersSandbox;

        public static IConfiguration ConfigInsurer1Development
        {
            get
            {
                return _configInsurer1Development ?? (_configInsurer1Development = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Slug = "test-insurer",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IklOU1VSRVIiLCJ1c2VyX25hbWUiOiJJTlNVUkVSLTEiLCJzY29wZSI6WyJyZWFkIiwid3JpdGUiXSwiZW50aXR5X2lkIjoxLCJhdXRob3JpdGllcyI6WyJST0xFX0lOU1VSRVIiXSwianRpIjoiZDVlM2ZjYWUtYjg5Yi00ZjI2LWFhMWUtYWU2NjcxZTk5YTk0Iiwic2x1ZyI6InRlc3QtaW5zdXJlciIsImNsaWVudF9pZCI6Im9zaWd1X2luc3VyZXJzX2FwcCJ9.zin1h7secwEYXCLJzKPVnyQiyo3otWDSoiVtkFY21PQ")
                });
            }
        }

        private static IConfiguration _configInsurer1Development;

        public static IConfiguration ConfigInsurer2Development
        {
            get
            {
                return _configInsurer2Development ?? (_configInsurer2Development = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Slug = "test-insurer-2",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IklOU1VSRVIiLCJ1c2VyX25hbWUiOiJJTlNVUkVSLTIiLCJzY29wZSI6WyJyZWFkIiwid3JpdGUiXSwiZW50aXR5X2lkIjoyLCJhdXRob3JpdGllcyI6WyJST0xFX0lOU1VSRVIiXSwianRpIjoiMjAyODNmYTMtOWY1Ny00OTFmLWI3MGUtYmY2OWU1N2ZhODg5Iiwic2x1ZyI6InRlc3QtaW5zdXJlci0yIiwiY2xpZW50X2lkIjoib3NpZ3VfaW5zdXJlcnNfYXBwIn0.3EOhYFU6OttPF_5Yob-S0yKy5gnF8NjLJ_qbsv_cU2Y")
                });
            }
        }

        private static IConfiguration _configInsurer2Development;

        public static IConfiguration ConfigProviderBranch1Development
        {
            get
            {
                return _configProviderBranch1Development ?? (_configProviderBranch1Development = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Slug = "test-provider",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlBST1ZJREVSX0JSQU5DSCIsInVzZXJfbmFtZSI6IlBST1ZJREVSX0JSQU5DSC0xIiwic2NvcGUiOlsicmVhZCIsIndyaXRlIl0sImVudGl0eV9pZCI6MSwiYXV0aG9yaXRpZXMiOlsiUk9MRV9QUk9WSURFUl9CUkFOQ0giXSwianRpIjoiNzdmMDM1YWQtMTRlMi00YzFlLTk4NmMtZWNjZDJiZGNlNzBkIiwic2x1ZyI6InRlc3QtcHJvdmlkZXIiLCJjbGllbnRfaWQiOiJvc2lndV9pbnN1cmVyc19hcHAifQ.vGAGxY90dJ1IcAgP8Nc4uG3aiQgEVKMVTjWtiVmn28I")
                });
            }
        }

        private static IConfiguration _configProviderBranch1Development;

        public static IConfiguration ConfigProviderBranch2Development
        {
            get
            {
                return _configProviderBranch2Development ?? (_configProviderBranch2Development = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Slug = "test-provider-2",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlBST1ZJREVSX0JSQU5DSCIsInVzZXJfbmFtZSI6IlBST1ZJREVSX0JSQU5DSC0yIiwic2NvcGUiOlsicmVhZCIsIndyaXRlIl0sImVudGl0eV9pZCI6MiwiYXV0aG9yaXRpZXMiOlsiUk9MRV9QUk9WSURFUl9CUkFOQ0giXSwianRpIjoiNTg4NTQ4ZWMtMzk1Yy00OTNiLTkxZjUtMGM3YmRiMDg2N2M4Iiwic2x1ZyI6InRlc3QtcHJvdmlkZXItMiIsImNsaWVudF9pZCI6Im9zaWd1X2luc3VyZXJzX2FwcCJ9.8MIFeIHWS_mYxPiol3p7HXGJDcXAMYLSUdtvA5VYWl8")
                });
            }
        }

        private static IConfiguration _configProviderBranch2Development;

        private static IConfiguration _configOsiguProduct;

        public static IConfiguration ConfigOsiguProduct
        {
            get
            {
                return _configOsiguProduct ?? (_configOsiguProduct = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Authentication = new Authentication("eyJhbGciOiJSUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlVTRVIiLCJ1c2VyX25hbWUiOiJlbGl1QG9zaWd1LmNvbSIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJleHAiOjE0NTk5OTEzNzksImVudGl0eV9pZCI6MiwiYXV0aG9yaXRpZXMiOlsiUk9MRV9XRUJfUFJPRFVDVF9BRE1JTiJdLCJqdGkiOiIwNmVkMWI2Yi1kZWJmLTQ2MzItYWUxZS1lNzEzYjM5ZDI0ZjIiLCJzbHVnIjpudWxsLCJjbGllbnRfaWQiOiJvc2lndV93ZWJfcHJvZHVjdHMifQ.zQjW_cTM2qGAfuy6fGw1OtWv0LjE91QqdF1wLWnMzaEZziUznFWquQM1EFjveHE1aPiOEPdRU1BsUP0Z_FNygLZaUhELk6DgJjaz7ndDPNckBLWq2v-vH8-uWcu2PGrAMw8IPxBRg8xqIMlIGox1DuhK0mXRJxgJnK21Ve4Eja0")
                });
            }
        }

        private static IConfiguration _localConfigSettlements;

        public static IConfiguration ConfigLocalSettlementsAPI
        {
            get
            {
                return _localConfigSettlements ?? (_localConfigSettlements = new Configuration
                {
                    BaseUrl = "http://localhost:5000",
                    Authentication =
                        new Authentication(
                            "eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlVTRVIiLCJ1c2VyX25hbWUiOiJVU0VSLTEiLCJzY29wZSI6WyJyZWFkIiwid3JpdGUiXSwiZW50aXR5X2lkIjoxLCJhdXRob3JpdGllcyI6WyJST0xFX0FVVEhfU0VSVkVSX0FQSV9BRE1JTiJdLCJqdGkiOiIzZjJjZTFlYS1lZDgwLTRiM2QtYmIwNi05ODAyYTQ2NDNmZTEiLCJzbHVnIjoiIiwiY2xpZW50X2lkIjoib3NpZ3VfaW5zdXJlcnNfYXBwIn0.ui7OI66qLdpo1k35J3Yq59ZOXCa2fOJj8UaDRO291No")

                });

            }
        }

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

       
    }
}
