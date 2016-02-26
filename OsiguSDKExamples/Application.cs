using OsiguSDK.Config;
using OsiguSDK.Infra;

namespace OsiguSDKExamples
{
    class Application
    {
        static void Main(string[] args)
        {
            IConfiguration _config = new Configuration(){
                BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                Slug = "test-insurer",
                Authentication = new Authentication("")                
            };

            var insurerAuhorizationRequests = new InsurersAuthorizationsRequests(_config);
            insurerAuhorizationRequests.CreateAuthorization();

        }
    }
}

