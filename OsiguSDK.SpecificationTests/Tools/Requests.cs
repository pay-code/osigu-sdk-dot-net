using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Products.Models;
using OsiguSDK.SpecificationTests.Settlements.Models;
using SubmitProductRequest = OsiguSDK.Providers.Models.Requests.SubmitProductRequest;


namespace OsiguSDK.SpecificationTests.Tools
{
    public class Requests
    {
        public static SubmitProductRequest SubmitProductRequest { get; set; }
        public static CreateClaimRequest CreateClaimRequest { get; set; }

        public static Insurers.Models.Requests.SubmitProductRequest SubmitInsurerProductRequest { get; set; }
        public static CreateAuthorizationRequest SubmitAuthorizationRequest { get; set; }

        public static OsiguProductRequest OsiguProductRequest { get; set; }
        public static Settlement SettlementRequest { get; set; }
    }
}