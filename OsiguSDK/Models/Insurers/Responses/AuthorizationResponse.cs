using System;
using Newtonsoft.Json;
using OsiguSDK.Models.Insurers.Base;

namespace OsiguSDK.Models.Insurers.Responses
{
    public class AuthorizationResponse : BaseAuthorization
    {
        /// <summary>
        /// Osigu authorization code
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Secure pin when creating a claim based 
        /// </summary>
        [JsonProperty(PropertyName = "pin")]
        public string Pin { get; set; }

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

        /// <summary>
        /// Status of the authorization
        /// </summary>
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

    }
}
