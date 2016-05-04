using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.ResponseModels
{
    public class DocumentDetail
    {
        [JsonProperty(PropertyName = "document_id")]
        public int DocumentId { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; } 
    }
}