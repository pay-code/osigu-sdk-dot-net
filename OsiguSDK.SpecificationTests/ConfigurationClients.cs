using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace OsiguSDK.SpecificationTests
{
    public class ConfigurationClients
    {
        public static IConfiguration ConfigInsurer1Development
        {
            get
            {
                return _configInsurer1Development ??
                       (_configInsurer1Development = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
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

        private static IConfiguration _configInsurer1Development;

        public static IConfiguration ConfigInsurer2Development
        {
            get
            {
                return _configInsurer2Development ??
                       (_configInsurer2Development = ConfigurationManager.AppSettings["TestingEnvironment"] == "DEV"
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

        private static IConfiguration _configInsurer2Development;

        public static IConfiguration ConfigProviderBranch1Development
        {
            get
            {
                return _configProviderBranch1Development ??
                       (_configProviderBranch1Development =
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

        private static IConfiguration _configProviderBranch1Development;

        public static IConfiguration ConfigProviderBranch2Development
        {
            get
            {
                return _configProviderBranch2Development ??
                       (_configProviderBranch2Development =
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

        private static IConfiguration _configProviderBranch2Development;

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
                               BaseUrl = ConfigurationManager.AppSettings["DevSettlementBaseUrl"],
                               Slug = ConfigurationManager.AppSettings["ConfigSettlementDevSlug"],
                               Authentication =
                                   new Authentication(ConfigurationManager.AppSettings["ConfigSettlementDevToken"])
                           }
                           : ConfigurationManager.AppSettings["TestingEnvironment"] == "PROD"
                               ? new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["ProductionBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigSettlementProdSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigSettlementProdToken"])
                               }
                               : new Configuration
                               {
                                   BaseUrl = ConfigurationManager.AppSettings["SettlementBaseUrl"],
                                   Slug = ConfigurationManager.AppSettings["ConfigSettlementSboxSlug"],
                                   Authentication =
                                       new Authentication(ConfigurationManager.AppSettings["ConfigSettlementSboxToken"])
                               });
            }
        }
    }
}