using Newtonsoft.Json;
using OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers.Models;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Insurers.Models
{
    public class RejectExpressAuthorizationRequest
    {
        [JsonProperty(PropertyName = "reason_id")]
        public int Reason { get; set; }
    }
}