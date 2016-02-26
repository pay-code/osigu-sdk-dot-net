using System.Reflection;
using log4net;
using Newtonsoft.Json;

namespace OsiguSDK.Core.Config
{
    public class Configuration : IConfiguration
    {
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        /// <summary>
        /// Initialize configuration object
        /// </summary>
        public Configuration()
        {
            Authentication = new Authentication.Authentication();
            BaseUrl = "https://api.paycodenetwork.com/v1";
        }

        /// <summary>
        /// Initialize configuration object using the 'OAUTH' Authentication </summary>
        /// <param name="accessToken"> - 'OAUTH' Authentication Access Token </param>
        public Configuration(string accessToken)
            : base() 
        {
            Authentication = new Authentication.Authentication {AccessToken = accessToken, Type = Core.Authentication.Authentication.AuthType.OAUTH};
        }

   
        /// <summary>
        /// Object containing Authentication data </summary>
        /// <returns> Authentication </returns>
        [JsonProperty(PropertyName = "authentication")]
        public Authentication.Authentication Authentication { get; set; }

        /// <summary>
        /// Base URL containing host name and port of the API server </summary>
        /// <returns> messagingBaseUrl </returns>
        [JsonProperty(PropertyName = "baseUrl")]
        public string BaseUrl { get; set; }

        /// <summary>
        /// Slug to access the resources
        /// </summary>
        [JsonProperty(PropertyName = "slug")]
        public string Slug { get; set; }
    }
}
