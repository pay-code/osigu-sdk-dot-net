using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Tax
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public decimal Name { get; set; }

        [JsonProperty(PropertyName = "country_code")]
        public decimal CountryCode { get; set; }

        [JsonProperty(PropertyName = "percentage")]
        public decimal Percentage { get; set; }
    }
}