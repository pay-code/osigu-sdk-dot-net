using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuxiliarModels
{
    public class Links
    {
        [JsonProperty(PropertyName = "self")]
        public Self Self { get; set; }
    }
}