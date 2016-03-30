using OsiguSDK.Insurers;
using System;
using FluentAssertions;
using OsiguSDK.Core.Models;
using OsiguSDK.Insurers.Clients;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Providers.Models;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products
{
    [Binding]
    public class InsurerGetListProductsSteps
    {
        private ProductsClient  _insurerClient {get; set;}
        [Given(@"I have the insurer products client")]
        public void GivenIHaveTheInsurerProductsClient()
        {
            //ScenarioContext.Current.Pending();
            _insurerClient = new ProductsClient(Tools.ConfigInsurer1Development);
        }
        
        [When(@"I make a request to the endpoint")]
        public void WhenIMakeARequestToTheEndpoint()
        {
            _insurerClient.GetListOfProducts();
        }
        
        [Then(@"the result should be status OK with code (.*) on the response")]
        public void ThenTheResultShouldBeStatusOKWithCodeOnTheResponse(int p0)
        {
            _insurerClient.Should().NotBeNull();
            _insurerClient.Should().Be(new Pagination<Product>(), "There should be a Pagination of products");
            //Pagination<Product> 
        }
    }
}
