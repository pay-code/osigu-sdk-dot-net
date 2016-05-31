using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers.Models
{
    public class AddItemsOfAnExpressAuthorizationRequest
    {
        [JsonProperty(PropertyName = "items")]
        public List<ExpressAuthorizationItem> ExpressAuthorizationItems { get; set; } 
    }
   
}
