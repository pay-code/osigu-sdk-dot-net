using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Prescription.Models.Requests.v1
{
    class StartingANewPrescriptionRequest
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "medical_service_id")]
        public long MedicalServiceId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "illness_onset")]
        public DateTime IllnessOnSet { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "insurer_company_id")]
        public int InsurerCompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "diagnoses")]
        public List<DiagnosesRequest> Diagnoses { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "illness_onset")]
        public string DateOfBirth { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<ItemsRequest> Items { get; set; }
   }
}
