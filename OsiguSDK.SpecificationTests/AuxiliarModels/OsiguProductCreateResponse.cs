using System;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuxiliarModels
{
    public class OsiguProductCreateResponse : OsiguProductRequest
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdateAt { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public Links Links { get; set; }
               
    }
}