using System;
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
        /// provider's invoice number
        /// </summary>
        [JsonProperty(PropertyName = "document_number")]
        public String DocumentNumber { get; set; }

        /// <summary>
        /// Date and time when the invoice was created
        /// </summary>
        [JsonProperty(PropertyName = "document_date")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// currency code used in the transaction
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public String Currency { get; set; }

        /// <summary>
        /// Amount to be claimed to the insurance
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Minimum amount to be purchased in order to be eligible for claim
        /// </summary>
        [JsonProperty(PropertyName = "minimum")]
        public decimal Minimum { get; set; }

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
        public List<ClaimItem> Items { get; set; }

        public class ClaimItem
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
            public int Quantity { get; set; }
            
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
