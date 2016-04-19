using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests
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
