using System;
using System.Configuration;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace OsiguSDK.SpecificationTests
{
    public static class Tools
    {
        public static readonly Fixture Fixture = new Fixture();

        public static ProductsClient ProductsProviderClient { get; set; }
        public static ProductsClient ProductsProductsClientWithNoPermission { get; set; }

        public static ClaimsClient ClaimsProviderClient { get; set; }
        public static ClaimsClient ClaimsProviderClientWithNoPermission { get; set; }

        public static QueueClient QueueProviderClient { get; set; }
        public static QueueClient QueueProviderClientWithNoPermission { get; set; }

        public static AuthorizationsClient ProviderAuthorizationClient { get; set; }
        public static Insurers.Clients.ProductsClient ProductsInsurerClient { get; set; }
        public static Insurers.Clients.AuthorizationsClient InsurerAuthorizationClient { get; set; }
        
        public static RestClient RestClient { get; set; }

        public static readonly string[] InsurerAssociatedProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] {"QAINSURER1", "QAINSURER2", "QAINSURER3"}
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] {"QAINSURER1_TEST", "QAINSURER2_TEST", "QAINSURER3_TEST"}
                    : new[] {"product1", "product2", "product3"};

        public static readonly string[] ProviderAssociateProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] {"QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3"}
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] {"QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3"}
                    : new[] {"product1", "product2", "product3"};

        public static readonly string[] OsiguProductId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? new[] {"1016241", "1016242", "1016243"}
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? new[] {"18551", "18552", "18553"}
                : new[] {"1", "2", "3"};

        public static readonly string RPNTestPolicyNumber = "50258433";
        public static readonly DateTime RPNTestPolicyBirthday = new DateTime(1967, 7, 20, 7, 0, 0, 123);
    }

    public class StringBuilder : ISpecimenBuilder
    {
        private readonly Random rnd = new Random();

        public object Create(object request, ISpecimenContext context)
        {
            var type = request as Type;

            if (type == null || type != typeof (string))
                return new NoSpecimen();

            return rnd.Next(0, 1000000).ToString();
        }
    }
}
