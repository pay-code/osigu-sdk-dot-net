using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.v1
{
    public class Diagnosis
    {
        /// <summary>
        /// Diagnosis information
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}