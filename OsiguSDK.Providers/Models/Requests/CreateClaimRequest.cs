using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests
{
    public class CreateClaimRequest
    {
        /// <summary>
        /// Authorization pin code for creating the claim
        /// </summary>
        [JsonProperty(PropertyName = "pin")]
        public string Pin { get; set; }

        /// <summary>
        /// List of claimed items 
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<ClaimItem> Items { get; set; }
        
        public class ClaimItem
        {
            /// <summary>
            /// provider's product code
            /// </summary>
            [JsonProperty(PropertyName = "product_id")]
            public string ProductId { get; set; }

            /// <summary>
            /// provider's product code, if the product was substituted
            /// </summary>
            [JsonProperty(PropertyName = "substitute_product_id")]
            public string SubstituteProductId { get; set; }
           
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
        }
    }
}
