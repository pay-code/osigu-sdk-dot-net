using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsiguSDK.Prescription.Models.Requests.v1
{
    class ItemsRequest
    {

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "product_type")]
        public string ProductType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "product_id")]
        public string ProductId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "dosage")]
        public int Dosage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "dosage_measurement_unit")]
        public string DosageMeasurementUnit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "frequency_in_hours")]
        public int FrequencyInHours { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "days_of_treatment")]
        public int DaysOfTreatment { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "instructions")]
        public string Instructions { get; set; }

    }
}
