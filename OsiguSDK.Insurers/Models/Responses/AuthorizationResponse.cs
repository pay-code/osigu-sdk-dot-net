using Newtonsoft.Json;
using OsiguSDK.Insurers.Models.Base;

namespace OsiguSDK.Insurers.Models.Responses
{
    public class AuthorizationResponse : BaseAuthorization
    {
        /// <summary>
        /// Osigu authorization code
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Secure pin when creating a claim based 
        /// </summary>
        [JsonProperty(PropertyName = "pin")]
        public string Pin { get; set; }

    
        /// <summary>
        /// Status of the authorization
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}
