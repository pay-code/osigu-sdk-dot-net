using System;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.V1
{
    public class Product
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
        /// Type of product [drug, exam, other]
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// status of the product
        /// </summary>
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
