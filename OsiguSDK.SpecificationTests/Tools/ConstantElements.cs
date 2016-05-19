using System;
using System.Configuration;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class ConstantElements
    {
        public static readonly string RPNTestPolicyNumber = "50258433";
        public static readonly DateTime RPNTestPolicyBirthday = new DateTime(1967, 7, 20, 7, 0, 0, 123);

        public static readonly int NoRetentionProviderId = ConfigurationManager.AppSettings["TestingEnvironment"] ==
                                                           "DEV"
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