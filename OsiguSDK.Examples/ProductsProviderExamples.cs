using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Clients;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;

namespace OsiguSDKExamples
{
    class ProductsProviderExamples
    {
        private ProductsClient _client;
        public ProductsProviderExamples(IConfiguration configuration)
        {
            _client = new ProductsClient(configuration);
        }

        public void SubmitProduct()
        {
            var request = new SubmitProductRequest
            {
                ProductId = "M215",
                Name = "Dexlansoprazole",
                FullName = "Dexlansoprazole 30 capsules of Dexilant 60mg",
                Manufacturer = "Takeda Pharmaceuticals"
            };
            _client.SubmitProduct(request);
        }

        public void SubmitRemoval(string productId)
        {
            _client.SubmitRemoval(productId);
        }

        public Product GetSingleProduct(string productId)
        {
            return _client.GetSingleProduct(productId);
        }


        public Pagination<Product> GetListOfProducts()
        {
            return _client.GetListOfProducts();
        }
    }
}
