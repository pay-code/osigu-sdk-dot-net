using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;

namespace OsiguSDKExamples
{
    class ProductsExample
    {
        private readonly ProductsClient _client;

        public ProductsExample(IConfiguration config)
        {
            _client = new ProductsClient(config);
        }

        public void SubmitProduct()
        {
            var request = new SubmitProductRequest
            {
                FullName = "Dexlansoprazole 30 capsules of Dexilant 60mg",
                Manufacturer = "Takeda Pharmaceuticals",
                Name = "Dexlansoprazole",
                ProductId = "M215",
                Type = "drug"
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
