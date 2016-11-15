﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    [ComVisible(true)]
    public class AuthorizationV2
    {
        /// <summary>
        /// Osigu authorization code
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

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
        /// Status of the authorization
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
            /// osigu authorized product code
            /// </summary>
            [JsonProperty(PropertyName = "osigu_product_id")]
            public string OsiguProductId { get; set; }

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
            public decimal Quantity { get; set; }

            /// <summary>
            /// Coinsurance percentage authorized
            /// </summary>
            [JsonProperty(PropertyName = "coinsurance_percentage")]
            public decimal CoinsurancePercentage { get; set; }

            /// <summary>
            /// Array of Substitutes
            /// </summary>
            [JsonProperty(PropertyName = "pbm_substitutes")]
            public List<PbmItem> PbmSubstitutes { get; set; }

            public class PbmItem

            {

                /// <summary>
                /// osigu authorized product code
                /// </summary>
                [JsonProperty(PropertyName = "product_id")]
                public string ProductId { get; set; }

                /// <summary>
                /// osigu authorized product code
                /// </summary>
                [JsonProperty(PropertyName = "name")]
                public string Name { get; set; }

                /// <summary>
                /// osigu authorized product code
                /// </summary>
                [JsonProperty(PropertyName = "quantity")]
                public decimal Quantity { get; set; }

                /// <summary>
                /// osigu authorized product code
                /// </summary>
                [JsonProperty(PropertyName = "coinsurance_percentage")]
                public decimal CoinsurancePercentage { get; set; }



            }
        }
    }
}
