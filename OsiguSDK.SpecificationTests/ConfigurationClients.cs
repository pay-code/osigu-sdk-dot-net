using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;

namespace OsiguSDK.SpecificationTests
{
    public class ConfigurationClients
    {
        public static IConfiguration ConfigInsurersSandbox
        {
            get
            {
                return _configInsurersSandbox ?? (_configInsurersSandbox = new Configuration
                {
                    BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                    Slug = "test-insurer",
                    Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
                });
            }
        }
        private static IConfiguration _configInsurersSandbox;

        public static IConfiguration ConfigProvidersSandbox
        {
            get
            {
                return _configProvidersSandbox ?? (_configProvidersSandbox = new Configuration
                {
                    BaseUrl = "https://sandbox.paycodenetwork.com/v1",
                    Slug = "test-insurer",
                    Authentication = new Authentication("589a4586628aac2815d20c1e17bc11ab")
                });
            }
        }

        private static IConfiguration _configProvidersSandbox;

        public static IConfiguration ConfigInsurer1Development
        {
            get
            {
                return _configInsurer1Development ?? (_configInsurer1Development = new Configuration
                {
                    BaseUrl = "https://api.paycodenetwork.com/v1",
                    Slug = "test-insurer",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IklOU1VSRVIiLCJ1c2VyX25hbWUiOiJJTlNVUkVSLTIiLCJzY29wZSI6WyJyZWFkIiwid3JpdGUiXSwiZW50aXR5X2lkIjoyLCJhdXRob3JpdGllcyI6WyJST0xFX0lOU1VSRVIiXSwianRpIjoiMWNkZDUwYWItMzZjZi00MWJjLWE2YmUtNmE3MjY1N2FlMmU4Iiwic2x1ZyI6InRlc3QtaW5zdXJlciIsImNsaWVudF9pZCI6Im9zaWd1X2luc3VyZXJzX2FwcCJ9.LYvZZRJJ51XsDKn-z6zTLsYcfQas7GtyjaX1EvQWPEc")
                });
            }
        }

        private static IConfiguration _configInsurer1Development;

        public static IConfiguration ConfigInsurer2Development
        {
            get
            {
                return _configInsurer2Development ?? (_configInsurer2Development = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Slug = "test-insurer-2",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IklOU1VSRVIiLCJ1c2VyX25hbWUiOiJJTlNVUkVSLTIiLCJzY29wZSI6WyJyZWFkIiwid3JpdGUiXSwiZW50aXR5X2lkIjoyLCJhdXRob3JpdGllcyI6WyJST0xFX0lOU1VSRVIiXSwianRpIjoiMjAyODNmYTMtOWY1Ny00OTFmLWI3MGUtYmY2OWU1N2ZhODg5Iiwic2x1ZyI6InRlc3QtaW5zdXJlci0yIiwiY2xpZW50X2lkIjoib3NpZ3VfaW5zdXJlcnNfYXBwIn0.3EOhYFU6OttPF_5Yob-S0yKy5gnF8NjLJ_qbsv_cU2Y")
                });
            }
        }

        private static IConfiguration _configInsurer2Development;

        public static IConfiguration ConfigProviderBranch1Development
        {
            get
            {
                return _configProviderBranch1Development ?? (_configProviderBranch1Development = new Configuration
                {
                    BaseUrl = "https://api.paycodenetwork.com/v1",
                    Slug = "test-provider",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlBST1ZJREVSX0JSQU5DSCIsInVzZXJfbmFtZSI6IlBST1ZJREVSX0JSQU5DSC0zOCIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJlbnRpdHlfaWQiOjM4LCJhdXRob3JpdGllcyI6WyJST0xFX1BST1ZJREVSX0JSQU5DSCJdLCJqdGkiOiIwOTFkNjBlNC01YzNiLTRlMDMtOTcxYS1kNWE0YTY4YWQwNjIiLCJzbHVnIjoidGVzdC1wcm92aWRlciIsImNsaWVudF9pZCI6Im9zaWd1X2luc3VyZXJzX2FwcCJ9.HREAyEw6ZyJq8j__iICWi1VOheRJWEn35MdnPCpl_cE")
                });
            }
        }

        private static IConfiguration _configProviderBranch1Development;

        public static IConfiguration ConfigProviderBranch2Development
        {
            get
            {
                return _configProviderBranch2Development ?? (_configProviderBranch2Development = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Slug = "test-provider-2",
                    Authentication = new Authentication("eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlBST1ZJREVSX0JSQU5DSCIsInVzZXJfbmFtZSI6IlBST1ZJREVSX0JSQU5DSC0yIiwic2NvcGUiOlsicmVhZCIsIndyaXRlIl0sImVudGl0eV9pZCI6MiwiYXV0aG9yaXRpZXMiOlsiUk9MRV9QUk9WSURFUl9CUkFOQ0giXSwianRpIjoiNTg4NTQ4ZWMtMzk1Yy00OTNiLTkxZjUtMGM3YmRiMDg2N2M4Iiwic2x1ZyI6InRlc3QtcHJvdmlkZXItMiIsImNsaWVudF9pZCI6Im9zaWd1X2luc3VyZXJzX2FwcCJ9.8MIFeIHWS_mYxPiol3p7HXGJDcXAMYLSUdtvA5VYWl8")
                });
            }
        }

        private static IConfiguration _configProviderBranch2Development;

        private static IConfiguration _configOsiguProduct;

        public static IConfiguration ConfigOsiguProduct
        {
            get
            {
                return _configOsiguProduct ?? (_configOsiguProduct = new Configuration
                {
                    BaseUrl = "https://dev.paycodenetwork.com/v1",
                    Authentication = new Authentication("eyJhbGciOiJSUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlVTRVIiLCJ1c2VyX25hbWUiOiJlbGl1QG9zaWd1LmNvbSIsInNjb3BlIjpbInJlYWQiLCJ3cml0ZSJdLCJleHAiOjE0NjA0ODE2ODAsImVudGl0eV9pZCI6MiwiYXV0aG9yaXRpZXMiOlsiUk9MRV9XRUJfUFJPRFVDVF9BRE1JTiJdLCJqdGkiOiI2ODg1MmFhZi02ZDc3LTQ3MTUtOTY1NC1lMmRkODcwZjE4NTciLCJzbHVnIjpudWxsLCJjbGllbnRfaWQiOiJvc2lndV93ZWJfcHJvZHVjdHMifQ.WQ6NPJ9-BslVCGgP22r9_xSFjmug4wye191wpCwOClOsffy2ajCfozpLrCf1ZDV4Fu-lGE9mtChOFPT2sNqsl3K_3NkCVlD0kI2kwytcPxClNz1A_47814RAetyNo6d6Jsm0iIQxlm3-nE-Sd7swsEyuL0Fq0bZFmN1EHDQSwkI")
                });
            }
        }

        private static IConfiguration _configSettlement;

        public static IConfiguration ConfigSettlement
        {
            get
            {
                return _configSettlement ?? (_configSettlement = new Configuration
                {
                    BaseUrl = "http://10.0.1.21:5000",
                    Slug = "fayco",
                    Authentication = new Authentication("adsfasdfasdfasdfasdf")
                });
            }
        }
    }
}