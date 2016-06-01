using System;
using OsiguSDK.Providers.Clients;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace OsiguSDK.SpecificationTests.Tools
{
    public static class TestClients
    {
        public static readonly Fixture Fixture = new Fixture();

        public static ProductsClient ProductsProviderClient { get; set; }
        public static ProductsClient ProductsProductsClientWithNoPermission { get; set; }

        public static ClaimsClient ClaimsProviderClient { get; set; }
        public static ClaimsClient ClaimsProviderClientWithNoPermission { get; set; }

        public static Insurers.Clients.ClaimsClient ClaimsInsurerClient { get; set; }
        public static Insurers.Clients.ClaimsClient ClaimsInsurerClientWithNoPermission { get; set; }

        public static QueueClient QueueProviderClient { get; set; }
        public static QueueClient QueueProviderClientWithNoPermission { get; set; }

        public static AuthorizationsClient ProviderAuthorizationClient { get; set; }
        public static Insurers.Clients.ProductsClient ProductsInsurerClient { get; set; }
        public static Insurers.Clients.AuthorizationsClient InsurerAuthorizationClient { get; set; }

        public static Insurers.Clients.ExpressAuthorizationClient InsurerExpressAuthorizationClient { get; set; }
        
        public static InternalRestClient InternalRestClient { get; set; }
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
