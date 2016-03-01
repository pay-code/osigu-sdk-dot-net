using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models
{
    public class ExpressAuthorization 
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string ProviderName { get; set; }

        [JsonProperty(PropertyName = "policy_holder")]
        public PolicyHolderInfo PolicyHolder { get; set; }
        
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        
        [JsonProperty(PropertyName = "items")]
        public List<ExpressAuthItem> Items { get; set; }
        
        [JsonProperty(PropertyName = "invoice")]
        public Invoice InvoiceDetails { get; set; }

        [JsonProperty(PropertyName = "minimum")]
        public decimal Minimum { get; set; }

        [JsonProperty(PropertyName = "copayment")]
        public decimal Copayment { get; set; }

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


        public class PolicyHolderInfo
        {
            /// <summary>
            /// Policy holder's unique identification code (combination of policy number + policy certificate or carnet number)
            /// </summary>
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }

            /// <summary>
            /// Date of birth of the policy holder
            /// </summary>
            [JsonProperty(PropertyName = "date_of_birth")]
            public DateTime DateOfBirth { get; set; }
        }

        public class ExpressAuthItem
        {
            /// <summary>
            /// Insurer product code
            /// </summary>
            [JsonProperty(PropertyName = "product_id")]
            public string ProductId { get; set; }
         
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

            [JsonProperty(PropertyName = "coinsurance_percentage")]
            public decimal CoInsurancePercentage { get; set; }
        }

        public class Invoice
        {
            [JsonProperty(PropertyName = "document_number")]
            public string DocumentNumber { get; set; }
            
            [JsonProperty(PropertyName = "document_date")]
            public DateTime DocumentDate { get; set; }

            [JsonProperty(PropertyName = "currency")]
            public string Currency { get; set; }

            [JsonProperty(PropertyName = "amount")]
            public decimal Amount { get; set; }
        }
    }


}
