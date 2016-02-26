using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsiguSDK.Insurers.Models.Base;

namespace OsiguSDK.Insurers.Models.Responses
{
    public class ClaimResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "document_number")]
        public String DocumentNumber { get; set; }

        [JsonProperty(PropertyName = "document_date")]
        public DateTime DocumentDate { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public String Currency { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public decimal Amount { get; set; }

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

        /// <summary>
        /// Authorized products or services
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<ClaimItemDetail> Items { get; set; }
    }
}
