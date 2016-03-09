using OsiguSDK.Core.Config;
using OsiguSDK.Core.Authentication;


namespace OsiguSDK.SpecificationTests
{
    public static class Tools
    {
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
    }
}
