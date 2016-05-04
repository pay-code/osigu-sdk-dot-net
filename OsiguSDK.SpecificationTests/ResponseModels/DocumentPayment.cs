using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.ResponseModels
{
    public class DocumentPayment
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "bank_name")]
        public string BankName { get; set; }

        [JsonProperty(PropertyName = "reference")]
        public string Reference { get; set; }
    }
}