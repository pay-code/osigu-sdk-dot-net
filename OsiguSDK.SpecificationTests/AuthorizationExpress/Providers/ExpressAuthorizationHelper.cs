using System;
using System.Linq;
using FluentAssertions;
using OsiguSDK.Core.Exceptions;
using OsiguSDK.Providers.Models;
using OsiguSDK.Providers.Models.Requests;
using OsiguSDK.SpecificationTests.Tools;

namespace OsiguSDK.SpecificationTests.AuthorizationExpress.Providers
{
    public class ExpressAuthorizationHelper
    {
        public ExpressAuthorizationTool ExpressAuthorizationTool { get; set; }

        public ExpressAuthorizationHelper()
        {
            ExpressAuthorizationTool = new ExpressAuthorizationTool(ConfigurationClients.ConfigProviderBranch1);
        }
        public string CreateExpressAuthorization()
        {
            string queuiId = null;
            var request = new CreateExpressAuthorizationRequest
            {
                InsurerId = ConstantElements.InsurerId.ToString(),
                PolicyHolder = ConstantElements.PolicyHolder
            };

            Utils.Dump("CreateExpressAuthorizationRequest: ", request);

            try
            {
                queuiId = ExpressAuthorizationTool.CreateExpressAuthorization(request);
                Utils.Dump("QueueId: ", queuiId);

            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            return queuiId;
        }

        public string CheckExpressAuthorizationStatus(string queueId)
        {
            Responses.ErrorId = 0;
            string expressAuthorizationId = null;
            try
            {
                expressAuthorizationId = ExpressAuthorizationTool.CheckQueueStatus(queueId);

                Utils.Dump("CheckExpressAuthorizationStatus: ", expressAuthorizationId);
            }
            catch (RequestException exception)
            {
                Responses.ErrorId = exception.ResponseCode;
            }

            return expressAuthorizationId;
        }

        public ExpressAuthorization AddItemsOfAnExpressAuthorization(string authorizationExpressId, int numberOfProducts)
        {
            CurrentData.ExpressAutorizationItems = ExpressAuthorizationTool.CreateProductList(numberOfProducts);

            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    ExpressAuthorizationTool.AddItemsOfAnExpressAuthorization(authorizationExpressId,
                        CurrentData.ExpressAutorizationItems);

                Utils.Dump("ExpressAuthorization Response: ", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("AddOrModifyItemsExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }

            return Responses.ExpressAuthorization;

        }

        public ExpressAuthorization CompleteExpressAuthorization(string authorizationExpressId, Invoice invoice)
        {
            Responses.ErrorId = 200;
            try
            {
                Responses.ExpressAuthorization =
                    ExpressAuthorizationTool.CompleteExpressAuthorization(Responses.ExpressAuthorizationId, invoice);

                Utils.Dump("AuthorizationId: " + authorizationExpressId + " Completed.", Responses.ExpressAuthorization);
            }
            catch (RequestException exception)
            {
                Utils.Dump("CompleteExpressAuthorization Exception: ", exception);
                Responses.ErrorId = exception.ResponseCode;
            }

            return Responses.ExpressAuthorization;
        }

        public void ValidateExpressAuthorizationResponse()
        {
            var coInsurancePercentage = Responses.ExpressAuthorization.Items.Average(x => x.CoInsurancePercentage) / 100;

            Responses.ExpressAuthorization.Items.ShouldAllBeEquivalentTo(CurrentData.ExpressAutorizationItems,
                x => x.Excluding(y => y.CoInsurancePercentage));

            Responses.ExpressAuthorization.Id.Should().Be(Responses.ExpressAuthorizationId);
            Responses.ExpressAuthorization.InsurerName.Should().Be(ConstantElements.InsurerName);
            Responses.ExpressAuthorization.PolicyHolder.ShouldBeEquivalentTo(ConstantElements.PolicyHolder);
            Responses.ExpressAuthorization.Copayment.Should().Be(0);
            Responses.ExpressAuthorization.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, 30000);
            Responses.ExpressAuthorization.UpdatedAt.Should().BeCloseTo(DateTime.UtcNow, 30000);

            //TODO: Remove the first round, the API must round it
            Math.Round(Responses.ExpressAuthorization.TotalCoInsurance, 2)
                .Should()
                .Be(Math.Round(CurrentData.ExpressAutorizationItems.Sum(x => x.Quantity * x.Price) * coInsurancePercentage, 2));

        }
    }
}