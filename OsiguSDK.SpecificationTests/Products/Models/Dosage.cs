using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuxiliarModels
{
    public class Dosage
    {
        [JsonProperty(PropertyName = "form")]
        public string Form { get; set; }

        [JsonProperty(PropertyName = "strength")]
        public string Strength { get; set; }

        [JsonProperty(PropertyName = "unit_of_measure")]
        public string UnitOfMeasure { get; set; } 
    }
}