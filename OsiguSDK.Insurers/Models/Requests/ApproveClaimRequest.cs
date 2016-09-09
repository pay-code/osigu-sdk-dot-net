using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace OsiguSDK.Insurers.Models.Requests
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
