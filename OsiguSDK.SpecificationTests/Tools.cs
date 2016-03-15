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

        public static IConfiguration ConfigInsurersDevelopment
        {
            get
            {
                return _configInsurersDevelopment ?? (_configInsurersDevelopment = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com",
                    Slug = "test-insurer",
                    Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
                });
            }
        }

        private static IConfiguration _configInsurersDevelopment;

        public static IConfiguration ConfigProvidersDevelopment
        {
            get
            {
                return _configProvidersDevelopment ?? (_configProvidersDevelopment = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com",
                    Slug = "fayco",
                    Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
                });
            }
        }

        private static IConfiguration _configProvidersDevelopment;
    }
}
