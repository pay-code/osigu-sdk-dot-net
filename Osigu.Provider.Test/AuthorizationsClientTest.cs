using Microsoft.VisualStudio.TestTools.UnitTesting;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;

namespace Osigu.Provider.Test
{
    [TestClass]
    public class AuthorizationsClientTest
    {
        [TestMethod]
        public void ShouldGetSingleAuthorization()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var authorizationClient = new AuthorizationsClient(config);
            var authorization = new Authorization();

                authorization = authorizationClient.GetSingleAuthorization("JFA826UQ0");
                Assert.IsInstanceOfType(authorization, typeof(Authorization));

        }
    }
}
