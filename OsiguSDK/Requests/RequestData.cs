using System.Net;
using RestSharp;

namespace OsiguSDK.Core.Requests
{
    public class RequestData
    {
        public const string FORM_URL_ENCOEDED_CONTENT_TYPE = "application/x-www-form-urlencoded; charset=utf-8";
        public const string JSON_CONTENT_TYPE = "application/json; charset=utf-8";

        public RequestData(string resourcePath, Method requestMethod)
        {
            ResourcePath = resourcePath;
            RequestMethod = requestMethod;
        }

        public RequestData(string apiUrl, Method requestMethod, object formParams)
            : this(apiUrl, requestMethod)
        {
            FormParams = formParams;
        }

        public RequestData(string resourcePath, Method requestMethod, string rootElement)
            : this(resourcePath, requestMethod)
        {
            RootElement = rootElement;
        }

        public RequestData(string resourcePath, Method requestMethod, string rootElement, object formParams)
            : this(resourcePath, requestMethod, rootElement)
        {
            FormParams = formParams;
        }

        public HttpWebRequest Request;
        public string ResourcePath;
        public Method RequestMethod;
        public string RootElement;
        public object FormParams;
        public string ContentType = JSON_CONTENT_TYPE; //FORM_URL_ENCOEDED_CONTENT_TYPE;
    }
}
