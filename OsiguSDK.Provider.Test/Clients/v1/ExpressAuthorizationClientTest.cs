using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OsiguSDK.Core.Authentication;
using OsiguSDK.Core.Config;
using OsiguSDK.Providers.Models.Requests.v1;
using OsiguSDK.Providers.Clients.v1;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;


namespace OsiguSDK.Provider.Test.Clients.v1
{
    [TestFixture]
    class ExpressAuthorizationClientTest
    {
        [Test]
        public void ShouldCreateAnExpressAuthorization()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var auth = fixture.Create<CreateExpressAuthorizationRequest>();

            var authentication = new Authentication()
            {
                AccessToken = "eyJhbGciOiJIUzI1NiJ9.eyJlbnRpdHlfdHlwZSI6IlBST1ZJREVSX0JSQU5DSCIsInVzZXJfbmFtZSI6IlBST1ZJREVSX0JSQU5DSC0zNTgiLCJzY29wZSI6WyJyZWFkIiwid3JpdGUiXSwiZW50aXR5X2lkIjozNTgsImF1dGhvcml0aWVzIjpbIlJPTEVfUFJPVklERVJfQlJBTkNIIl0sImp0aSI6IjE0M2RjNGJmLTk1ODUtNGViYy1hMzZjLTY0YmZiMDU1ZmYxZSIsInNsdWciOiJtZXlrb3MiLCJjbGllbnRfaWQiOiJvc2lndV9pbnN1cmVyc19hcHAifQ.8vx-5csLMMmYvATMEX4utEOaIxeE47T2TBaEC1eFcJc",
            };

            var configuration = new Configuration()
            {
                Slug = "meykos",
                Authentication = authentication,
                BaseUrl = "https://api.paycodenetwork.com/v1"
            };

            var express = new ExpressAuthorizationClient(configuration);
                

            string url = express.CreateExpressAuthorization(auth);

            Assert.IsNotEmpty(url);
            Console.WriteLine(url);

            


        }
    }
}
