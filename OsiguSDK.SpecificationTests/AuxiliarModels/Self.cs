using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuxiliarModels
{
    public class Self
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }
}