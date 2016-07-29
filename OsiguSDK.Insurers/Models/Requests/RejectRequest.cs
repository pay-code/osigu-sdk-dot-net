using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.Requests
{
    public class RejectRequest
    {
        /// <summary>
        /// Insurer's rejection reason extracted from 
        /// rejection reasons api
        /// </summary>
        [JsonProperty(PropertyName = "reason_id")]
        public int RejectionReasonId { get;set; }
    }
}
