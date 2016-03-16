using TechTalk.SpecFlow;
using OsiguSDK.Insurers.Models;
using OsiguSDK.Insurers.Models.Requests;
using OsiguSDK.Insurers.Clients;
using FluentAssertions;
using NHibernate.Util;
using System.Collections.Generic;
using System.Linq;

namespace OsiguSDK.SpecificationTests
{
    [Binding]
    public class ExampleTestSteps
    {
        ProductsClient _productClient { get; set; }
        SubmitProductRequest _request { get; set; }
        Product _response { get; set; }

        [Given(@"I am a valid user")]
        public void GivenIAmAValidUser()
        {
            _productClient = new ProductsClient(Tools.ConfigInsurersSandbox);
        }
        
        [Given(@"I have the values to my request")]
        public void GivenIHaveTheValuesToMyRequest()
        {
            _request = new SubmitProductRequest
            {
                FullName = "Dexlansoprazole 30 capsules of Dexilant 60mg",
                Manufacturer = "Takeda Pharmaceuticals",
                Name = "Dexlansoprazole",
                ProductId = "M215",
                Type = "drug"
            };
        }
        
        [When(@"I request the endpoint")]
        public void WhenIRequestTheEndpoint()
        {
            _response = _productClient.SubmitProduct(_request);
        }
        
        [Then(@"the result should the expected")]
        public void ThenTheResultShouldTheExpected()
        {
            _response.Should().NotBeNull();
            _response.Should().Be(new Product(), "there should be a new product");
            _response.Name.Should().Be(_request.Name,"{0} was the name I send to the request",_request.Name);
            _response.Status.Should().NotBeEmpty();
            _response.Status.Should()
                .Be("Pending Review", "the product should be inserted correctly {0} {1}", "asdf", "qwerty");
        }
        [Given(@"I have the values (.*) to my request")]
        public void GivenIHaveTheValuesToMyRequest(int p0)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the possible results should be the expected")]
        public void ThenThePossibleResultsShouldBeTheExpected(Table scenarios)
        {
            var scenario = scenarios.Rows.ToList().First();
            var x = scenario["ExpectedResult"];
            var y = scenario[0];
        }
    }
}
