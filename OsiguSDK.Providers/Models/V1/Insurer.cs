using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    public class Insurer
    {
        /// <summary>
        /// insurer's unique identification code
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Insurer's name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
