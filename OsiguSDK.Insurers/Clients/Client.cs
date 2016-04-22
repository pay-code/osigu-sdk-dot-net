using OsiguSDK.Core.Config;

namespace OsiguSDK.Insurers.Clients
{
    public class Client
    {
        public Client(IConfiguration configuration)
        {
            Authorizations = new AuthorizationsClient(configuration);
            Claims = new ClaimsClient(configuration);
            ExpressAuthorizations= new ExpressAuthorizationClient(configuration);
            Products = new ProductsClient(configuration);
        }

        public AuthorizationsClient Authorizations { get; }

        public ClaimsClient Claims { get; }

        public ExpressAuthorizationClient ExpressAuthorizations { get; }
        
        public ProductsClient Products { get; }
        
    }
}
