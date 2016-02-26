using Newtonsoft.Json;

namespace OsiguSDK.Models.Insurers
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