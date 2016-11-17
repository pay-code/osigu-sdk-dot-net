using System;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.Requests.v1
{
    public class ApproveClaimRequest
    {
        /// <summary>
        /// Assigned Settlement Date
        /// </summary>
        [JsonProperty(PropertyName = "assigned_settlement_date")]
        public DateTime AssignedSettlementDate{ get; set; }
    }
}
