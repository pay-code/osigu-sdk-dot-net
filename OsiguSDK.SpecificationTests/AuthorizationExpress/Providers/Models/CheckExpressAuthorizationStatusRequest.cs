using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class CheckExpressAuthorizationStatusRequest
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "eta")]
        public string Eta { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

    }
}