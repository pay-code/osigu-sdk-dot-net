using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.Requests.v1
{
    public class ExpressRejectRequest
    {
        /// <summary>
        /// Insurer's rejection reason extracted from 
        /// rejection reasons api
        /// </summary>
        [JsonProperty(PropertyName = "reason_id")]
        public int RejectionReasonId { get;set; }
    }
}
