using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients;

namespace OsiguSDK.Providers.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var _config = Configuration.LoadFromFile("provider-test.json");
            var _client = new Client(_config);


            //GET A SINGLE PRE-AUTHORIZATION
            var authResult = _client.Authorizations.GetSingleAuthorization("GT-8KTA-ledu");
            

            //GET 




        }
    }
}
