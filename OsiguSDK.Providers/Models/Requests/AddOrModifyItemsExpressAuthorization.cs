using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests
{
    public class AddOrModifyItemsExpressAuthorization
    {
       
        /// <summary>
        /// List of claimed items 
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<Item> Items { get; set; }

        public class Item
        {
            /// <summary>
            /// provider's product code
            /// </summary>
            [JsonProperty(PropertyName = "product_id")]
            public string ProductId { get; set; }

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
        }
    }
}
