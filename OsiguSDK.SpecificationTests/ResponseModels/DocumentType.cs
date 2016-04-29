using System;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.ResponseModels
{
    public class DocumentType
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "account_action")]
        public string AccountAction { get; set; }

        [JsonProperty(PropertyName = "invoiceable")]
        public bool Invoiceable { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "money_flow")]
        public string MoneyFlow { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
    }
}