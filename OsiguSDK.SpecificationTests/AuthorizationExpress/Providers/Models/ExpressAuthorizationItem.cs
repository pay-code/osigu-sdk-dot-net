using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class ExpressAuthorizationItem
    {
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }
    }
}