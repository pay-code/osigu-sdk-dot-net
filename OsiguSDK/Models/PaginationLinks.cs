using Newtonsoft.Json;

namespace OsiguSDK.Core.Models
{
    public class PaginationLinks
    {
        [JsonProperty(PropertyName = "first")]
        public HrefLink First { get; set; }

        [JsonProperty(PropertyName = "next")]
        public HrefLink Next { get; set; }

        [JsonProperty(PropertyName = "prev")]
        public HrefLink Prev { get; set; }

        [JsonProperty(PropertyName = "last")]
        public HrefLink Last { get; set; }

        [JsonProperty(PropertyName = "current")]
        public HrefLink Current { get; set; }
    }
    
    public class HrefLink
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }
}