using System;
using Newtonsoft.Json;

namespace OsiguSDK.Insurers.Models
{
    public class Policy
    {

        /// <summary>
        /// 2-character IS0-3166-1 code (EJ. GT, US)
        /// </summary>
        [JsonProperty(PropertyName = "country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Policy number
        /// </summary>
        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        /// <summary>
        /// Policy certificate number
        /// </summary>
        [JsonProperty(PropertyName = "certificate")]
        public string Certificate { get; set; }

        /// <summary>
        /// Policy expiration date
        /// </summary>
        [JsonProperty(PropertyName = "expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [JsonProperty(PropertyName = "insurance_company_name")]
        public string InsuranceCompanyName { get; set; }

        [JsonProperty(PropertyName = "insurance_company_code")]
        public string InsuranceCompanyCode { get; set; }

        /// <summary>
        /// Policy holder information
        /// </summary>
        [JsonProperty(PropertyName = "policy_holder")]
        public PolicyHolderInfo PolicyHolder { get; set; }

        public class PolicyHolderInfo
        {
            /// <summary>
            /// Policy holder's unique identification code (combination of policy number + policy certificate or carnet number)
            /// </summary>
            [JsonProperty(PropertyName = "id")]
            public string Id { get; set; }


            /// <summary>
            /// name of the beneficiary
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            /// <summary>
            /// Email of the policy holder
            /// </summary>
            [JsonProperty(PropertyName = "email")]
            public string Email { get; set; }

            /// <summary>
            /// Cellphone for SMS content (optional)
            /// </summary>
            [JsonProperty(PropertyName = "cellphone")]
            public string Cellphone { get; set; }

            /// <summary>
            /// Is this the policy owner
            /// </summary>
            [JsonProperty(PropertyName = "owner")]
            public bool Owner { get; set; }

            /// <summary>
            /// Date of birth of the policy holder
            /// </summary>
            [JsonProperty(PropertyName = "date_of_birth")]
            public DateTime DateOfBirth { get; set; }
        }
    }
}