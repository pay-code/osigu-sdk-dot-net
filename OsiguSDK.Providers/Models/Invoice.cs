using System;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    public class Invoice
    {
        /// <summary>
        /// Provider's invoice number
        /// </summary>
        [JsonProperty(PropertyName = "document_number")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Provider's invoice date
        /// </summary>
        [JsonProperty(PropertyName = "document_date")]
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// currency code
        /// </summary>
        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }
        
        /// <summary>
        /// invoice amount
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public Decimal Amount { get; set; }
    }
}
