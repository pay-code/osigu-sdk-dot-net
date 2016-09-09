using System;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    public class Diagnosis
    {
        /// <summary>
        /// Dianosis ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string ProductId { get; set; }

        /// <summary>
        /// Diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

    }
}
