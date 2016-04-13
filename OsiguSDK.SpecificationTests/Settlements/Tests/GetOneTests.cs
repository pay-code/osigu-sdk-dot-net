using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using FluentAssertions;
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
            var response = Client.RequestToEndpoint<SettlementResponse>(Method.GET, "/settlements/0");
        }

        [Test]
        public void ValidateSuccess()
        {
            var response = Client.RequestToEndpoint<SettlementResponse>(Method.GET, "/settlements/5");
            response.Id.Should().Be(5);
            response.Status.Should().Be("CREATED");
            response.Type.Should().Be("CASHOUT");
            response.TotalAmount.Should().Be(1000000);
            response.TotalDiscounts.Should().Be(20000);
            response.CurrencyCode.Should().Be("502");
            response.RevenueShare.Should().Be(0);

            #region TaxesExpected
            var taxesExpected = new List<Taxes>()
            {
                new Taxes
                {
                    Id = 4,
                    Amount = 2142.85m,
                    Percentage = 12m,

                }
            };
            #endregion

            #region Items Expexted

            var itemsExpexted = new List<Item>()
            {
                new Item
                {
                   ClaimId = 103,
                   ClaimAmount = 1000000,
                   Id = 53,
                }
            };
            #endregion

            #region Commissions Expected

            var commissionsExpected = new List<Comission>()
            {
                new Comission
                {
                    Id = 4,
                    Amount = 20000m,
                    Percentage = 2,
                    ComissionType = "NORMAL"
                }
            };
            #endregion

            response.Taxes.ShouldAllBeEquivalentTo(taxesExpected, o=>o.Excluding(x=>x.CreatedAt).Excluding(x=>x.UpdatedAt).Excluding(x=>x.Comission).Excluding(x=>x.Tax));
            response.Items.ShouldAllBeEquivalentTo(itemsExpexted, o=>o.Excluding(x=>x.CreatedAt).Excluding(x=>x.UpdatedAt));
            response.Comissions.ShouldAllBeEquivalentTo(commissionsExpected, o=>o.Excluding(x=>x.InvoiceId).Excluding(x=>x.CreatedAt).Excluding(x=>x.UpdatedAt));

        }

       
    }
}

