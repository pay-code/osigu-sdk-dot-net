using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsiguSDK.Prescription.Models.Requests.v1
{
    class RegisterAServiceRequest
    {

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "insurer_company_id")]
        public string InsurerCompanyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "medical_license")]
        public string MedicalLicence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "policy_holder")]
        public PolicyHoldersRequest PolicyHolder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "execute_claim")]
        public string ExecuteClaim { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "additional_information")]
        public string AdditionalInformation { get; set; }

    }
}
