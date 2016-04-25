using System.Configuration;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using Configuration = OsiguSDK.Core.Config.Configuration;

namespace OsiguSDK.SpecificationTests
{
    public class ConfigurationClients
    {
        public static IConfiguration ConfigInsurer1
        {
            get
            {
                return _configInsurer1 ??
                       (_configInsurer1 = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                           ? new Configuration
                           {
                               BaseUrl = ConfigurationManager.AppSettings["DevelopmentBaseUrl"],
                               Slug = ConfigurationManager.AppSettings["ConfigInsurer1DevSlug"],
                               Authentication =
                                   new Authentication(ConfigurationManager.AppSettings["ConfigInsurer1DevToken"])
                           }
                           : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["ProductionBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigInsurer1ProdSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigInsurer1ProdToken"])
                               }
                               : new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["SandboxBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigInsurer1SboxSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigInsurer1SboxToken"])
                               });
            }
        }

        private static IConfiguration _configInsurer1;

        public static IConfiguration ConfigInsurer2
        {
            get
            {
                return _configInsurer2 ??
                       (_configInsurer2 = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                           ? new Configuration
                           {
                               BaseUrl = ConfigurationManager.AppSettings["DevelopmentBaseUrl"],
                               Slug = ConfigurationManager.AppSettings["ConfigInsurer2DevSlug"],
                               Authentication =
                                   new Authentication(ConfigurationManager.AppSettings["ConfigInsurer2DevToken"])
                           }
                           : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["ProductionBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigInsurer2ProdSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigInsurer2ProdToken"])
                               }
                               : new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["SandboxBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigInsurer2SboxSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigInsurer2SboxToken"])
                               });
            }
        }

        private static IConfiguration _configInsurer2;

        public static IConfiguration ConfigProviderBranch1
        {
            get
            {
                return _configProviderBranch1 ??
                       (_configProviderBranch1 =
                           ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["DevelopmentBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigProvider1DevSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigProvider1DevToken"])
                               }
                               : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                                   ? new Configuration
                                   {
                                       BaseUrl = ConfigurationManager.AppSettings["ProductionBaseUrl"],
                                       Slug = ConfigurationManager.AppSettings["ConfigProvider1ProdSlug"],
                                       Authentication =
                                           new Authentication(
                                               ConfigurationManager.AppSettings["ConfigProvider1ProdToken"])
                                   }
                                   : new Configuration
                                   {
                                       BaseUrl = ConfigurationManager.AppSettings["SandboxBaseUrl"],
                                       Slug = ConfigurationManager.AppSettings["ConfigProvider1SboxSlug"],
                                       Authentication =
                                           new Authentication(
                                               ConfigurationManager.AppSettings["ConfigProvider1SboxToken"])
                                   });
            }
        }

        private static IConfiguration _configProviderBranch1;

        public static IConfiguration ConfigProviderBranch2
        {
            get
            {
                return _configProviderBranch2 ??
                       (_configProviderBranch2 =
                           ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["DevelopmentBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigProvider2DevSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigProvider2DevToken"])
                               }
                               : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                                   ? new Configuration
                                   {
                                       BaseUrl = ConfigurationManager.AppSettings["ProductionBaseUrl"],
                                       Slug = ConfigurationManager.AppSettings["ConfigProvider2ProdSlug"],
                                       Authentication =
                                           new Authentication(
                                               ConfigurationManager.AppSettings["ConfigProvider2ProdToken"])
                                   }
                                   : new Configuration
                                   {
                                       BaseUrl = ConfigurationManager.AppSettings["SandboxBaseUrl"],
                                       Slug = ConfigurationManager.AppSettings["ConfigProvider2SboxSlug"],
                                       Authentication =
                                           new Authentication(
                                               ConfigurationManager.AppSettings["ConfigProvider2SboxToken"])
                                   });
            }
        }

        private static IConfiguration _configProviderBranch2;

        private static IConfiguration _configOsiguProduct;

        public static IConfiguration ConfigOsiguProduct
        {
            get
            {
                return _configOsiguProduct ??
                       (_configOsiguProduct =
                           ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["DevelopmentBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigOsiguDevSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigOsiguDevToken"])
                               }
                               : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                                   ? new Configuration
                                   {
                                       BaseUrl = ConfigurationManager.AppSettings["ProductionBaseUrl"],
                                       Slug = ConfigurationManager.AppSettings["ConfigOsiguProdSlug"],
                                       Authentication =
                                           new Authentication(
                                               ConfigurationManager.AppSettings["ConfigProvider2ProdToken"])
                                   }
                                   : new Configuration
                                   {
                                       BaseUrl = ConfigurationManager.AppSettings["SandboxBaseUrl"],
                                       Slug = ConfigurationManager.AppSettings["ConfigOsiguSboxSlug"],
                                       Authentication =
                                           new Authentication(
                                               ConfigurationManager.AppSettings["ConfigProvider2SboxToken"])
                                   });
            }
        }

        private static IConfiguration _configSettlement;

        public static IConfiguration ConfigSettlement
        {
            get
            {
                return _configSettlement ??
                       (_configSettlement = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
                           ? new Configuration
                           {
                               BaseUrl = ConfigurationManager.AppSettings["SettlementDevBaseUrl"],
                               Slug = ConfigurationManager.AppSettings["ConfigSettlementDevSlug"],
                               Authentication =
                                   new Authentication(ConfigurationManager.AppSettings["ConfigSettlementDevToken"])
                           }
                           : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["SettlementProdBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigSettlementProdSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigSettlementProdToken"])
                               }
                               : new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["SettlementSboxBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigSettlementSboxSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigSettlementSboxToken"])
                               });
            }
        }
    }
}