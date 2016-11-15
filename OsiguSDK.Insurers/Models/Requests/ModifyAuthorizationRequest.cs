using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsiguSDK.Insurers.Models.V1;

namespace OsiguSDK.Insurers.Models.Requests
{
    public class ModifyAuthorizationRequest 
    {
        
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
            /// quantity authorized
            /// </summary>
            [JsonProperty(PropertyName = "quantity")]
            public decimal Quantity { get; set; }
        }
    }
}
