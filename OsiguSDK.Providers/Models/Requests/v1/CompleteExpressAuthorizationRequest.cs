using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests.v1
{
    public class CompleteExpressAuthorizationRequest
    {
        /// <summary>
        /// Invoice data
        /// </summary>
        [JsonProperty(PropertyName = "invoice")]
        public Invoice Invoice { get; set; }
        
    }
}
