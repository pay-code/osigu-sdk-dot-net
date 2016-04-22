using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests
{
    public class CompleteClaimRequest
    {
        /// <summary>
        /// Invoice data
        /// </summary>
        [JsonProperty(PropertyName = "invoice")]
        public Invoice Invoice { get; set; }
        
    }
}
