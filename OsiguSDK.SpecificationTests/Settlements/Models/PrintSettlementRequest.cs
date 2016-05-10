using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class PrintSettlementRequest
    {
        /// <summary>
        /// Claims to include in the settlement
        /// </summary>
        [JsonProperty(PropertyName = "settlements")]
        public List<int> Claims { get; set; }
    }
}