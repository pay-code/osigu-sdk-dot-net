using System;
using System.Collections.Generic;
using System.Configuration;
using OsiguSDK.Providers.Models.Requests;

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

        public static readonly string InsurerName = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? "test-insurer"
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? "test-insurer2"
                : "test-insurer";

        public static readonly int ProviderBranchId = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? int.Parse(ConfigurationManager.AppSettings["ProviderBranchIdDev"])
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? int.Parse(ConfigurationManager.AppSettings["ProviderBranchIdProd"])
                : int.Parse(ConfigurationManager.AppSettings["ProviderBranchIdSbox"]);

        public static readonly string ProviderBranchName = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
            ? ConfigurationManager.AppSettings["ProviderBranchNameDev"]
            : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                ? ConfigurationManager.AppSettings["ProviderBranchNameProd"]
                : ConfigurationManager.AppSettings["ProviderBranchNameSbox"];

        public static PolicyHolderInfo PolicyHolder => new PolicyHolderInfo
        {
            DateOfBirth = DateTime.Parse("1980/02/14"),
            Id = "502338037"
        };
    }

}