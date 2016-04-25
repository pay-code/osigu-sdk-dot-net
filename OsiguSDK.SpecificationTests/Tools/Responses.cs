using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using Claim = OsiguSDK.Providers.Models.Claim;

namespace OsiguSDK.SpecificationTests.Tools
{
    public class Responses
    {
        public static Claim Claim { get; set; }
        public static Invoice Invoice { get; set; }

        public static int ErrorId { get; set; }
        public static int ErrorId2 { get; set; }
        public static RequestException RequestException { get; set; }

        public static string QueueId { get; set; }
        public static QueueStatus QueueStatus { get; set; }

        public static string AuthorizationId { get; set; }
        public static string PIN { get; set; }
    }
}