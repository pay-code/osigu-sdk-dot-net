using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Taxes
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        [JsonProperty(PropertyName = "percentage")]
        public decimal Percentage { get; set; }

        [JsonProperty(PropertyName = "comission")]
        public Comission Comission { get; set; }

        [JsonProperty(PropertyName = "tax")]
        public Tax Tax { get; set; } 
    }
}