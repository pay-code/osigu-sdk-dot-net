using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Products.Models
{
    public class OsiguProductRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "dosage")]
        public Dosage Dosage { get; set; }

        [JsonProperty(PropertyName = "active_ingredient")]
        public string ActiveIngredient { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public decimal Quantity { get; set; }

        [JsonProperty(PropertyName = "generic")]
        public bool Generic { get; set; }

        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}