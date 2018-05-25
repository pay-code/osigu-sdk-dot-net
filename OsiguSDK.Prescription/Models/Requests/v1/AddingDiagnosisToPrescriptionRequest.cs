using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsiguSDK.Prescription.Models.Requests.v1
{
    class AddingDiagnosisToPrescriptionRequest
    {

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "diagnosis_code")]
        public string DiagnosisCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
    }
}
