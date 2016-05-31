using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class ExpressAuthorizationResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "insurer")]
        public string Insurer { get; set; }

        [JsonProperty(PropertyName = "policy_holder")]
        public PolicyHolderExpressAuthorization PolicyHolder { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<ExpressAuthorizationItemResponse> ExpressAuthorizationItems { get; set; }

        [JsonProperty(PropertyName = "invoice")]
        public ExpressAuthorizationInvoice Invoice { get; set; }

        [JsonProperty(PropertyName = "copayment")]
        public string Copayment { get; set; }
        
        [JsonProperty(PropertyName = "total_coinsurance")]
        public decimal TotalCoinsurance { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

    public class ExpressAuthorizationItemResponse : ExpressAuthorizationItem
    {
        [JsonProperty(PropertyName = "coinsurance_percentage")]
        public int CoinsurancePercentage { get; set; }
    }
}