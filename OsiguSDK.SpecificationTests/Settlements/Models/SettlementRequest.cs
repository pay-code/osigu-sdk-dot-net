using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
   

    public class SettlementRequest
    {
        /// <summary>
        /// Begining date of settlement
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public DateTime From { get; set; }

        /// <summary>
        /// Ending date of settlements
        /// </summary>
        [JsonProperty(PropertyName = "to")]
        public DateTime To { get; set; }

        /// <summary>
        /// Id of insurer related to the settlement
        /// </summary>
        [JsonProperty(PropertyName = "insurer_id")]
        public string InsurerId { get; set; }

        /// <summary>
        /// Id of provider related to the settlement
        /// </summary>
        [JsonProperty(PropertyName = "provider_id")]
        public string ProviderId { get; set; }

        /// <summary>
        /// Claims related to the settlement
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<SettlementItemRequest> SettlementsItems { get; set; }
        }

    public class SettlementItemRequest
    {
        /// <summary>
        /// Id of the claim
        /// </summary>
        [JsonProperty(PropertyName = "claim_id")]
        public string ClaimId { get; set; }
    }
}
