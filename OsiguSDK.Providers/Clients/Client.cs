using OsiguSDK.Core.Config;

namespace OsiguSDK.Providers.Clients
{
    public class Client
    {
        public Client(IConfiguration configuration)
        {
            Authorizations = new AuthorizationsClient(configuration);
            Claims = new ClaimsClient(configuration);
            ExpressAuthorizations= new ExpressAuthorizationClient(configuration);
            Insurers = new InsurersClient(configuration);
            Products = new ProductsClient(configuration);
            Queue = new QueueClient(configuration);
        }

        public AuthorizationsClient Authorizations { get; }

        public ClaimsClient Claims { get; }

        public ExpressAuthorizationClient ExpressAuthorizations { get; }

        public InsurersClient Insurers { get; }

        public ProductsClient Products { get; }

        public QueueClient Queue { get; }
    }
}
