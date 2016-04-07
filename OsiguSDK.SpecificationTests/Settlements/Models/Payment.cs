using System;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Payment
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "bank_name")]
        public string BankName { get; set; }

        [JsonProperty(PropertyName = "payments")]
        public string ReferenceNumber { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}