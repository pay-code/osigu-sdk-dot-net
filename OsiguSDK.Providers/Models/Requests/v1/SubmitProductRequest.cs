using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests.v1
{
    public class SubmitProductRequest
    {
        /// <summary>
        /// Insurer's product's unique identification code
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Insurer's product's brand
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Insurer's product's full name and description
        /// </summary>
        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Product manufacturer name
        /// </summary>
        [JsonProperty(PropertyName = "manufacturer")]
        public string Manufacturer { get; set; }

        /// <summary>
        /// Product Type --> Drug , Laboratory
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
