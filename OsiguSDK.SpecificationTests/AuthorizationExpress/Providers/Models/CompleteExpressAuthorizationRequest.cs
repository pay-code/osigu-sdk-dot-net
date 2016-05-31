using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class CompleteExpressAuthorizationRequest
    {
        [JsonProperty(PropertyName = "invoice")]
        public ExpressAuthorizationInvoice Invoice { get; set; }
    }
}