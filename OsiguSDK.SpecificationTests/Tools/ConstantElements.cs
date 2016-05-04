using System;
using System.Configuration;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class ConstantElements
    {
        public static readonly string[] InsurerAssociatedProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] { "QAINSURER1", "QAINSURER2", "QAINSURER3" }
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] { "QAINSURER1_TEST", "QAINSURER2_TEST", "QAINSURER3_TEST" }
                    : new[] { "product1", "product2", "product3" };

        public static readonly string[] ProviderAssociateProductId =
            ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                ? new[] { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" }
                : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                    ? new[] { "QAPROVIDER1", "QAPROVIDER2", "QAPROVIDER3" }
                    : new[] { "product1", "product2", "product3" };

        public static readonly string[] OsiguProductId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? new[] { "1016241", "1016242", "1016243" }
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? new[] { "18551", "18552", "18553" }
                : new[] { "1", "2", "3" };

        public static readonly string RPNTestPolicyNumber = "50258433";
        public static readonly DateTime RPNTestPolicyBirthday = new DateTime(1967, 7, 20, 7, 0, 0, 123);

        public static readonly int NoRetentionProviderId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV" 
            ? 1 
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD" 
                ? 2 
                : 3;

        public static readonly int RetentionProviderId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? 2
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? 3
                : 4;

        public static readonly int InsurerId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? 1
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? 2
                : 3;


    }

}