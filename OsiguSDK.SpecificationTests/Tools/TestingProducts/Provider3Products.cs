using System.Configuration;

namespace OsiguSDK.SpecificationTests.Tools.TestingProducts
{
    public class Provider3Products
    {
        public static readonly string[] InsurerAssociatedProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] { "QAINSURER1PRD4", "QAINSURER1PRD5", "QAINSURER1PRD6" }
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] { "QAINSURER1_TEST", "QAINSURER2_TEST", "QAINSURER3_TEST" }
                    : new[] { "product1", "product2", "product3" };

        public static readonly string[] ProviderAssociateProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] { "QAPROVIDER3PRD1", "QAPROVIDER3PRD2", "QAPROVIDER3PRD3" }
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" }
                    : new[] { "product1", "product2", "product3" };

        public static readonly string[] OsiguProductId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? new[] { "1028871", "1028872", "1028873" }
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? new[] { "18551", "18552", "18553" }
                : new[] { "1", "2", "3" };

    }
}