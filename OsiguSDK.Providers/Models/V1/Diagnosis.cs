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
        public long Id { get; set; }

        /// <summary>
        /// Diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }

    }
}
