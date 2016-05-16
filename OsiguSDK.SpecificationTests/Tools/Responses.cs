using System.Collections.Generic;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Models;
using OsiguSDK.SpecificationTests.ResponseModels;
using OsiguSDK.SpecificationTests.Settlements.Models;
using Claim = OsiguSDK.Providers.Models.Claim;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class Responses
    {
        public static Claim Claim { get; set; }
        public static Invoice Invoice { get; set; }

        public static int ErrorId { get; set; }
        public static int ErrorId2 { get; set; }
        public static string ResponseMessage { get; set; }
        public static RequestException RequestException { get; set; }

        public static string QueueId { get; set; }
        public static QueueStatus QueueStatus { get; set; }

        public static Insurers.Models.Authorization Authorization { get; set; }

        public static Insurers.Models.Claim InsurerClaim { get; set; }

        public static Pagination<DocumentType> DocumentTypes { get; set; }
        public static DocumentType DocumentType { get; set; }
        public static DocumentResponse Document { get; set; }
        public static SettlementResponse Settlement { get; set; }
        public static IList<RequestError.ValidationError> Errors { get; set; }
        public static Pagination<SettlementResponse> Settlements { get; set; }
        public static PrintSettlementResponse PrintSettlementResponse { get; set; }
    }
}