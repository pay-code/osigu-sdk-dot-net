using Newtonsoft.Json;

namespace OsiguSDK.Providers.Models
{
    public class QueueStatus
    {
         public enum QueueStatusEnum
         {
            PENDING,
            COMPLETED,
            ERROR,
            WORKING,
            DENIED
         }

        [JsonProperty(PropertyName = "status")]
        public QueueStatusEnum Status { get; set; }

        [JsonProperty(PropertyName = "eta")]
        public string ETA { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string Error { get; set; }

        [JsonIgnore]
        public string ResourceId { get; set; }
    }

   
}
