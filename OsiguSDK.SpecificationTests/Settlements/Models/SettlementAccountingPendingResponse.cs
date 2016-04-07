using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class SettlementAccountingPendingResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "insurer_id")]
        public int InsurerId { get; set; }

        [JsonProperty(PropertyName = "provider_id")]
        public int ProviderId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "comissions")]
        public List<Comission> Comissions { get; set; }

        [JsonProperty(PropertyName = "taxes")]
        public List<Taxes> Taxes { get; set; }

        [JsonProperty(PropertyName = "tax_retentions")]
        public List<TaxRetentions> TaxRetentions { get; set; }
    }
}