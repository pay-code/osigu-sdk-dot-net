using System;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class PolicyHolderExpressAuthorization
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "date_of_birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
