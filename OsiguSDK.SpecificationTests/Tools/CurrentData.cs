using System.Collections.Generic;
using OsiguSDK.Providers.Models.Requests;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class CurrentData
    {
        public static List<Providers.Models.Claim> Claims { get; set; }
        public static List<AddOrModifyItemsExpressAuthorization.Item> ExpressAutorizationItems { get; set; }
    }
}