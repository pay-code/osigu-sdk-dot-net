using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using OsiguSDK.Core.Config;
using OsiguSDK.Core.Models;
using OsiguSDK.Providers.Clients.v1;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests.v1;
using Assert = NUnit.Framework.Assert;

namespace Osigu.Provider.Test
{
    [TestClass]
    public class InsurersClientTest
    {
        [TestMethod]
        public void ShouldGetInsurers()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var insurersClient = new InsurersClient(config);
            var insurer = new Pagination<Insurer>();

                insurer = insurersClient.GetInsurers();
                Assert.IsNotNull(insurer);
        }
    }
}
