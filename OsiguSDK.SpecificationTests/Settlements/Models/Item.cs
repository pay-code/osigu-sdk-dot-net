using System;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "claim_amount")]
        public decimal ClaimAmount { get; set; }

        [JsonProperty(PropertyName = "claim")]
        public Claim Claim { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; } 
    }
}