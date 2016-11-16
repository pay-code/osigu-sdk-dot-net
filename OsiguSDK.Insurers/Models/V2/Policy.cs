using System;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace OsiguSDK.Insurers.Models.v2
{
    public class Policy : v1.Policy
    {
        /// <summary>
        /// Insurer's pbm Enabled
        /// </summary>
        [JsonProperty(PropertyName = "pbm_enabled")]
        public Boolean PbmEnabled { get; set; }

        /// <summary>
        /// Insurer's pbm Enabled
        /// </summary>
        [JsonProperty(PropertyName = "pbm_plan")]
        public string PbmPlan { get; set; }

        /// <summary>
        /// Coinsurances
        /// </summary>
        [JsonProperty(PropertyName = "coinsurances")]
        public List<CoinsurancesInfo> Coinsurances { get; set; }

        public class CoinsurancesInfo
        {

            /// <summary>
            /// Coinsurance Name
            /// </summary>
            [JsonProperty(PropertyName = "name")]
            public string Name { get; set; }

            /// <summary>
            /// Coinsurance Value
            /// </summary>
            [JsonProperty(PropertyName = "value")]
            public decimal Value { get; set; }
        }


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