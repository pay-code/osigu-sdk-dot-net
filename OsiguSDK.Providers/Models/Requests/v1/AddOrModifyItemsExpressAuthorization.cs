using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests.v1
{
    public class AddOrModifyItemsExpressAuthorization
    {

        /// <summary>
        /// Dianosis given
        /// </summary>
        [JsonProperty(PropertyName = "Diagnoses")]

        public List<Diagnosis> Diagnoses { get; set; }


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

            /// <summary>
            /// product price
            /// </summary>
            [JsonProperty(PropertyName = "coinsurance_percentage")]
            public decimal CoinsurancePercentage { get; set; }

            /// <summary>
            /// List of substitute items 
            /// </summary>
            [JsonProperty(PropertyName = "pbm_substitutes")]
            public List<ItemPbm> PbmSubstitutes { get; set; }

            public class ItemPbm
            {
                /// <summary>
                /// provider's product code
                /// </summary>
                [JsonProperty(PropertyName = "product_id")]
                public string ProductId { get; set; }

                /// <summary>
                /// provider's product code
                /// </summary>
                [JsonProperty(PropertyName = "name")]
                public string Name { get; set; }

                /// <summary>
                /// provider's product code
                /// </summary>
                [JsonProperty(PropertyName = "quantity")]
                public decimal Quantity { get; set; }

                /// <summary>
                /// provider's product code
                /// </summary>
                [JsonProperty(PropertyName = "coinsurance_percentage")]
                public decimal CoinsurancePercentage { get; set; }
            }
        }
    }
}
