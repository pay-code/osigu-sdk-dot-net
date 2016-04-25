using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Products.Models;
using OsiguSDK.SpecificationTests.Tools;
using Ploeh.AutoFixture;
using RestSharp;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Products.Osigu
{
    [Binding]
    public class CreateOsiguProductSteps
    {
        [Given(@"I have configured the rest client")]
        public void GivenIHaveConfiguredTheRestClient()
        {
            TestClients.GenericRestClient = new GenericRestClient(ConfigurationClients.ConfigOsiguProduct);
        }
        
        [Given(@"I have the request to create an osigu product")]
        public void GivenIHaveTheRequestToCreateAnOsiguProduct()
        {
            Requests.OsiguProductRequest = TestClients.Fixture.Create<OsiguProductRequest>();
            Console.WriteLine(Requests.OsiguProductRequest);
        }
        
        [When(@"I request the create osigu product endpoint")]
        public void WhenIRequestTheCreateOsiguProductEndpoint()
        {
            try
            {
                var a = TestClients.GenericRestClient.RequestToEndpoint<object>(Method.POST, "/products", Requests.OsiguProductRequest);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }
            
        }
        
        [Then(@"the result should be the osigu product created with the respective id")]
        public void ThenTheResultShouldBeTheOsiguProductCreatedWithTheRespectiveId()
        {
            Responses.ErrorId.Should().Be(0);
            
        }
    }
}
