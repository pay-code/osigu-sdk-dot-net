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
        public string Id { get; set; }

        /// <summary>
        /// Diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }
}
