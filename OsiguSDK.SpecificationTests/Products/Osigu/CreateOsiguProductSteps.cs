﻿using System;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.SpecificationTests.Products.Models;
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
            Tools.RestClient = new RestClient(ConfigurationClients.ConfigOsiguProduct);
        }
        
        [Given(@"I have the request to create an osigu product")]
        public void GivenIHaveTheRequestToCreateAnOsiguProduct()
        {
            Requests.OsiguProductRequest = Tools.Fixture.Create<OsiguProductRequest>();
            Console.WriteLine(Requests.OsiguProductRequest);
        }
        
        [When(@"I request the create osigu product endpoint")]
        public void WhenIRequestTheCreateOsiguProductEndpoint()
        {
            try
            {
                var a = Tools.RestClient.RequestToEndpoint<object>(Method.POST, "/products", Requests.OsiguProductRequest);
            }
            catch (RequestException exception)
            {
                Tools.ErrorId = exception.ResponseCode;
            }
            
        }
        
        [Then(@"the result should be the osigu product created with the respective id")]
        public void ThenTheResultShouldBeTheOsiguProductCreatedWithTheRespectiveId()
        {
            Tools.ErrorId.Should().Be(0);
            
        }
    }
}
