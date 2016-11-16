using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models.v1
{
    public class Doctor
    {
        /// <summary>
        /// Country code of the doctor
        /// </summary>
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Name of the doctor
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Medical license number or identification
        /// </summary>
        [JsonProperty(PropertyName = "medical_license")]
        public string MedicalLicense { get; set; }

        /// <summary>
        /// List of specialties
        /// </summary>
        [JsonProperty(PropertyName = "specialties")]
        public List<DoctorSpecialty> Specialties { get; set; }

        public class DoctorSpecialty
        {

            /// <summary>
            /// Speciality description
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }
        }
    }
}