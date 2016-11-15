using OsiguSDK.Core.Config;

namespace OsiguSDK.Insurers.Clients.V1
{
    public class Client
    {
        public Client(IConfiguration configuration)
        {
            Authorizations = new AuthorizationsClient(configuration);
            Claims = new ClaimsClient(configuration);
            ExpressAuthorizations= new ExpressAuthorizationClient(configuration);
            Products = new ProductsClient(configuration);
            Settlements = new SettlementsClient(configuration);
        }

        public AuthorizationsClient Authorizations { get; }

        public ClaimsClient Claims { get; }

        public ExpressAuthorizationClient ExpressAuthorizations { get; }
        
        public ProductsClient Products { get; }

        public SettlementsClient Settlements { get; }

    }
}
