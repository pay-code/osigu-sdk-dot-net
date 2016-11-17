using System.Text;
using OsiguSDK.Core.Client;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Core.Requests;
using OsiguSDK.Insurers.Models.v1;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Insurers.Models.Requests.v1;
using RestSharp;

namespace OsiguSDK.Insurers.Clients.v1
{
    public class ProductsClient : BaseClient
    {
        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
        }

        public void SubmitNew(SubmitProductRequest request)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/products");
            var requestData = new RequestData(urlBuilder.ToString(), Method.POST, null, request);
            ExecuteMethod(requestData);
        }

        public void SubmitRemoval(string productId)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/products/").Append(productId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.DELETE, null, null);
            ExecuteMethod(requestData);
        }

        public Product GetSingleProduct(string productId)
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/products/").Append(productId);
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Product>(requestData);
        }


        public Pagination<Product> GetListOfProducts()
        {
            var urlBuilder = new StringBuilder("/v1/insurers/").Append(Configuration.Slug).Append("/products");
            var requestData = new RequestData(urlBuilder.ToString(), Method.GET, null, null);
            return ExecuteMethod<Pagination<Product>>(requestData);
        }

    }
}
