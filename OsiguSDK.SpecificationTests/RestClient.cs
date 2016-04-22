using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Requests;
using RestSharp;

namespace OsiguSDK.SpecificationTests
{
    public class RestClient : BaseClient
    {
        public RestClient(IConfiguration configuration) : base(configuration)
        {
        }

        public T RequestToEndpoint<T>(Method method, string url, dynamic request)
        {
            var requestData = new RequestData(url, method, null, request);
            return ExecuteMethod<T>(requestData);
        }

        public void RequestToEndpoint(Method method, string url, dynamic request)
        {
            var requestData = new RequestData(url, method, null, request);
            ExecuteMethod(requestData);
        }

        public T RequestToEndpoint<T>(Method method, string url)
        {
            var requestData = new RequestData(url, method, null, null);
            return ExecuteMethod<T>(requestData);
        }

        public void RequestToEndpoint(Method method, string url)
        {
            var requestData = new RequestData(url, method, null, null);
            ExecuteMethod(requestData);
        }
    }
}