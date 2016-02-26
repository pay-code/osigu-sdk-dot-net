using Newtonsoft.Json;

namespace OsiguSDK.Models.Insurers.Base
{
    public class ClaimItemDetail : ItemDetail
    {
        [JsonProperty(PropertyName = "substitute_product_id")]
        public string SubstituteProductId { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "coinsurance_percentage")]
        public decimal CoInsurancePercentage { get; set; }
    }
}
