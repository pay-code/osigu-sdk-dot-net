using Newtonsoft.Json;

namespace OsiguSDK.Core.Authentication
{
    public class Authentication
    {
        public enum AuthType
        {
            OAUTH
        }

        public Authentication()
        {
            Type = AuthType.OAUTH;
        }


        /// <summary>
        /// Initialize 'OAUTH' Authentication </summary>
        /// <param name="accessToken"> </param>
        public Authentication(string accessToken)
        {
            AccessToken = accessToken;
            Type = AuthType.OAUTH;
        }

        /// <summary>
        /// Authentication type </summary>
        /// <returns> AuthType </returns>
        [JsonProperty(PropertyName = "type")]
        public AuthType Type;
             
        /// <summary>
        /// 'OAUTH' Authentication Access Token </summary>
        /// <returns> String </returns>
        [JsonProperty(PropertyName = "accessToken")]
        public string AccessToken;
    }
}
