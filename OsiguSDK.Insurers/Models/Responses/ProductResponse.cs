using System;
using Newtonsoft.Json;
using OsiguSDK.Insurers.Models.Base;

namespace OsiguSDK.Insurers.Models.Responses
{
    public class ProductResponse : BaseProduct
    {
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Date and time when the resource was created
        /// </summary>
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time when the resource was last updated
        /// </summary>
        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}
