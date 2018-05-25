using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Prescription.Models.Requests.v1
{
    class UpdatePrescriptionDiagnosesItemsRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "illness_onset")]
        public DateTime IllnessOnSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "diagnoses")]
        public List<DiagnosesRequest> Diagnoses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<ItemsRequest> InsurerCompanyId { get; set; }
   }
}
