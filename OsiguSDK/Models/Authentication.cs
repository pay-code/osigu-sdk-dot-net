using Newtonsoft.Json;

namespace OsiguSDK.Models
{
    public class Authentication
    {
        public enum AuthType
        {
            //BASIC,
            OAuth
        }

        public Authentication()
            : base()
        {
            Type = AuthType.OAuth;
        }


        /// <summary>
        /// Initialize 'OAUTH' Authentication </summary>
        /// <param name="accessToken"> </param>
        public Authentication(string accessToken)
        {
            AccessToken = accessToken;
            Type = AuthType.OAuth;
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
