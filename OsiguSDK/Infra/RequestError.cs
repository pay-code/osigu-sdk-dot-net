using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.Infra
{
    class RequestError
    {

        [JsonProperty(PropertyName = "code")]
        public String Code { get; set; }

        [JsonProperty(PropertyName = "message")]
        public String Message { get; set; }

        [JsonProperty(PropertyName = "errors")]
        public List<ValidationError> Errors { get; set; }

        public class ValidationError
        {
            [JsonProperty(PropertyName = "path")]
            public String Path { get; set; }

            [JsonProperty(PropertyName = "message")]
            public String Message { get; set; }
        }
    }
}
