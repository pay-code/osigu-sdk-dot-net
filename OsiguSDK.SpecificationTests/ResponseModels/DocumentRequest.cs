using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.ResponseModels
{
    public class DocumentRequest
    {
        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "details")]
        public DocumentDetail Detail { get; set; }

        [JsonProperty(PropertyName = "payments")]
        public List<DocumentPayment> Payments { get; set; }
    }
}