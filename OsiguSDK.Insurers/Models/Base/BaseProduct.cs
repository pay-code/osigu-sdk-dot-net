using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.Base
{
    public abstract class BaseProduct
    {
        [JsonProperty(PropertyName = "id")]
        public string ProductId { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
    }
}
