using System;
using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models.Requests.v1
{
    public class CreateExpressAuthorizationRequest
    {
        /// <summary>
        /// Insurer's unique identification code 
        /// </summary>
        [JsonProperty(PropertyName = "insurer_id")]
        public string InsurerId { get; set; }

        /// <summary>
        /// Policy holder information
        /// </summary>
        [JsonProperty(PropertyName = "policy_holder")]
        public PolicyHolderInfo PolicyHolder { get; set; }

    }


    public class PolicyHolderInfo
    {
        /// <summary>
        /// Policy holder's unique identification code (combination of policy number + policy certificate or carnet number)
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Date of birth of the policy holder
        /// </summary>
        [JsonProperty(PropertyName = "date_of_birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
