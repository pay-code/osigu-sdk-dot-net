using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;

namespace OsiguSDK.Providers.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var _config = new Configuration("123");
            var authClient = new AuthorizationsClient(_config);
            var productClient = new ProductsClient(_config);
            var claimsClient = new ClaimsClient(_config);
            var expressAuthClient = new ExpressAuthorizationClient(_config);

            //authClient.


        }
    }
}
