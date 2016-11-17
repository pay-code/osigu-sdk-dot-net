using OsiguSDK.Core.Config;

namespace OsiguSDK.Insurers.Clients.v1
{
    public class Client
    {
        public Client(IConfiguration configuration)
        {
            Authorizations = new AuthorizationsClient(configuration);
            Claims = new ClaimsClient(configuration);
            ExpressAuthorizations= new ExpressAuthorizationsClient(configuration);
            Products = new ProductsClient(configuration);
            Settlements = new SettlementsClient(configuration);
        }

        public AuthorizationsClient Authorizations { get; }

        public ClaimsClient Claims { get; }

        public ExpressAuthorizationsClient ExpressAuthorizations { get; }
        
        public ProductsClient Products { get; }

        public SettlementsClient Settlements { get; }

    }
}
