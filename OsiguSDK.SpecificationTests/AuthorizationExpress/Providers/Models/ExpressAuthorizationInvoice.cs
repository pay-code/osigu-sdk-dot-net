using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class ExpressAuthorizationInvoice
    {
        [JsonProperty(PropertyName = "document_number")]
        public string DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "document_date")]
        public string DocumentDate { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public string Amount { get; set; }
    }
}