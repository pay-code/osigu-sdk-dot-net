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
    public class DiagnosesClientTest
    {
        [TestMethod]
        public void ShouldGetListOfDiagnoses()
        {
            var config = Configuration.LoadFromFile("provider-test.json");
            var diagnosesClient = new DiagnosesClient(config);
            var diagnosis = new Pagination<Diagnosis>();

                diagnosis = diagnosesClient.GetListOfDiagnoses("lumbalgi");
                
                Assert.IsNotNull(diagnosis);


        }
    }
}
