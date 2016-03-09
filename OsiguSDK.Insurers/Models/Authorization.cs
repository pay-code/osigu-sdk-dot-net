using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models
{
    public class Authorization
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
        /// Insurer's unique authorization code/number 
        /// </summary>
        [JsonProperty(PropertyName = "reference_id")]
        public string ReferenceId { get; set; }

        /// <summary>
        /// Date and time when authorized
        /// </summary>
        [JsonProperty(PropertyName = "authorization_date")]
        public DateTime AuthorizationDate { get; set; }

        /// <summary>
        /// Date and time when expires
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime ExpiresAt { get; set; }

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

        /// <summary>
        /// Medical diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "diagnoses")]
        public List<Diagnosis> Diagnoses { get; set; }


        /// <summary>
        /// Doctor responsible of the diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "doctor")]
        public Doctor Doctor { get; set; }

        /// <summary>
        /// Policy information
        /// </summary>
        [JsonProperty(PropertyName = "policy")]
        public Policy Policy { get; set; }

        /// <summary>
        /// Authorized products or services
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<Item> Items { get; set; }

        public class Item
        {
            /// <summary>
            /// insurer authorized product code
            /// </summary>
            [JsonProperty(PropertyName = "product_id")]
            public string ProductId { get; set; }

            /// <summary>
            /// Product name
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            /// <summary>
            /// quantity authorized
            /// </summary>
            [JsonProperty(PropertyName = "quantity")]
            public int Quantity { get; set; }
        }

    
     

    }
}
