using System;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class CreateExpressAuthorizationRequest
    {
        [JsonProperty(PropertyName = "insurer_id")]
        public int InsurerId { get; set; }

        [JsonProperty(PropertyName = "policy_holder")]
        public PolicyHolderExpressAuthorization PolicyHolder { get; set; }
    }
}