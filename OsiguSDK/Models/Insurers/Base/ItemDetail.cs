using Newtonsoft.Json;

namespace OsiguSDK.Models.Insurers.Base
{
    public class ItemDetail
    {

        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

    }
}