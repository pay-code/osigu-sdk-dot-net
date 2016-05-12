using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class PrintSettlementResponse
    {
        public class PrintSettlementRequest
        {
            /// <summary>
            /// Id of file created
            /// </summary>
            [JsonProperty(PropertyName = "file_id")]
            public string FileId { get; set; }

            /// <summary>
            /// Path of file created
            /// </summary>
            [JsonProperty(PropertyName = "path")]
            public string Path { get; set; }
        }
    }
}