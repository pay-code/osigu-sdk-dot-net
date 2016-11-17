using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.Requests.v1
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
