using System.Configuration;

namespace OsiguSDK.SpecificationTests.Tools.TestingProducts
{
    public class Provider2Products
    {
        public static readonly string[] InsurerAssociatedProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] { "QAINSURER1PRD1", "QAINSURER1PRD2", "QAINSURER1PRD3" }
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] { "QAINSURER1_TEST", "QAINSURER2_TEST", "QAINSURER3_TEST" }
                    : new[] { "product1", "product2", "product3" };

        public static readonly string[] ProviderAssociateProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] { "QAPROVIDER2PRD1", "QAPROVIDER2PRD2", "QAPROVIDER2PRD3" }
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" }
                    : new[] { "product1", "product2", "product3" };

        public static readonly string[] OsiguProductId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? new[] { "1028868", "1028869", "1028870" }
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? new[] { "18551", "18552", "18553" }
                : new[] { "1", "2", "3" };
        
    }
}