using System;
using System.Configuration;
using System.Net;
using NHibernate.Linq.Clauses;
using NUnit.Framework;
using OsiguSDK.Core.Config;
using OsiguSDK.SpecificationTests.Settlements.Models;
using RestSharp;

namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    [TestFixture]
    public class GetOneTests : BaseTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            Client = new RestClient(new Configuration
            {
                BaseUrl = Tools.BaseUrl
            });
        }


        [Test]
        public void ValidateAuthenticationError()
        {

        }

        [Test]
        public void ValidateInvalidSettlementId()
        {

        }

        [Test]
        public void ValidateSuccess()
        {
            var response = Client.RequestToEndpoint<SettlementResponse>(Method.GET, "/settlements/1");

           
        }
    }
}