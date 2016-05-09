using System;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Retentions
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }
        [JsonProperty(PropertyName = "percentage")]
        public decimal Percentage { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}