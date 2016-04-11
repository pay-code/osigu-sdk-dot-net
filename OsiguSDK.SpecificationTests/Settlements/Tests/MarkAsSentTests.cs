using NUnit.Framework;
using OsiguSDK.Core.Config;
using RestSharp;

namespace OsiguSDK.SpecificationTests.Settlements.Tests
{
    [TestFixture]
    public class MarkAsSentTests : BaseTest
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
        public void ValidateResponseWithInvalidSettlementId()
        {
            
        }

        [Test]
        public void ValidateSuccessResponse()
        {
            var response = Client.RequestToEndpoint<object>(Method.PUT, "/settlements/2/sent");
            
        }
    }
}