using System;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    public class Diagnoses
    {
        /// <summary>
        /// Dianosis ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public decimal Id { get; set; }

        /// <summary>
        /// Diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

    }
}
