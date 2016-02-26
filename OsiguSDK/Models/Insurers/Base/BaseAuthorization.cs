using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Models.Insurers.Base
{
    public abstract class BaseAuthorization
    {
        /// <summary>
        /// Insurer's unique authorization code/number 
        /// </summary>
        [JsonProperty(PropertyName = "reference_id")]
        public string ReferenceId { get; set; }

        /// <summary>
        /// Date and time when authorized
        /// </summary>
        [JsonProperty(PropertyName = "authorization_date")]
        public DateTime AuthorizationDate { get; set; }

        /// <summary>
        /// Date and time when expires
        /// </summary>
        [JsonProperty(PropertyName = "expires_at")]
        public DateTime ExpiresAt { get; set; }

        /// <summary>
        /// Medical diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "diagnoses")]
        public List<Diagnosis> Diagnoses { get; set; }


        /// <summary>
        /// Doctor responsible of the diagnosis
        /// </summary>
        [JsonProperty(PropertyName = "doctor")]
        public DoctorInfo DoctorInfo { get; set; }

        /// <summary>
        /// Policy information
        /// </summary>
        [JsonProperty(PropertyName = "policy")]
        public Policy Policy { get; set; }


        /// <summary>
        /// Authorized products or services
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public List<ItemDetail> Items { get; set; }
    }
}
