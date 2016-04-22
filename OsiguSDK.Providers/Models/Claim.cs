﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    public class Claim
    {
        /// <summary>
        /// claim identification number
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// provider's invoice details
        /// </summary>
        [JsonProperty(PropertyName = "invoice")]
        public Invoice Invoice { get; set; }
        
        /// <summary>
        /// fixed amount of copayment the insured will need to pay
        /// </summary>
        [JsonProperty(PropertyName = "copayment")]
        public decimal Copayment { get; set; }

        /// <summary>
        ///  Amount calculated of the coinsurance that the insured will need to pay
        /// </summary>
        [JsonProperty(PropertyName = "total_coinsurance")]
        public decimal TotalCoInsurance { get; set; }

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
        /// Authorized products or services
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<Item> Items { get; set; }

        public class Item
        {
            /// <summary>
            /// Insurer product code
            /// </summary>
            [JsonProperty(PropertyName = "product_id")]
            public string ProductId { get; set; }

            /// <summary>
            /// Insurer's product code, if the product was substituted
            /// </summary>
            [JsonProperty(PropertyName = "substitute_product_id")]
            public string SubstituteProductId { get; set; }

            /// <summary>
            /// Product name
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            /// <summary>
            /// quantity claimed
            /// </summary>
            [JsonProperty(PropertyName = "quantity")]
            public decimal Quantity { get; set; }
            
            /// <summary>
            /// product price
            /// </summary>
            [JsonProperty(PropertyName = "price")]
            public decimal Price { get; set; }
            
            /// <summary>
            /// percentage amount that the insured will need to pay for this product / service
            /// </summary>
            [JsonProperty(PropertyName = "coinsurance_percentage")]
            public decimal CoInsurancePercentage { get; set; }
        }
    }
}
