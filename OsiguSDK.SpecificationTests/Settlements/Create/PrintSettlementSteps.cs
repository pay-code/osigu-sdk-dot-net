using System;
using System.Linq;
using System.Windows.Forms;
using OsiguSDK.SpecificationTests.Settlements.Models;
using OsiguSDK.SpecificationTests.Tools;
using RestSharp;
using ServiceStack.Text;
using TechTalk.SpecFlow;

namespace OsiguSDK.SpecificationTests.Settlements.Create
{
    [Binding]
    public class PrintSettlementSteps
    {
        public bool WithFormat { get; set; }

        [Then(@"result should be ok")]
        public void ThenResultShouldBeOk()
        {
            
        }
        
        [Given(@"I have '(.*)' settlement with (.*) claims with amount '(.*)'")]
        public void GivenIHaveSettlementWithClaimsWithAmount(string settlementType, int numberOfClaimsToCreate, string claimAmountRange)
        {
            Requests.SettlementType = Utils.ParseEnum<SettlementType>(settlementType);
            var claimsAmountRange = Utils.ParseEnum<ClaimAmountRange>(claimAmountRange);

            Responses.Settlement = SettlementTool.CreateSettlement(Requests.SettlementType, numberOfClaimsToCreate, claimsAmountRange);
            Utils.Dump("Claims details", CurrentData.Claims.Select(x => new { x.Id, x.Invoice.Amount }));
            Utils.Dump("Settlement Created", Responses.Settlement);
            
        }

        [Given(@"I specify a valid format '(.*)' for to print")]
        public void GivenISpecifyAValidFormatForToPrint(string format)
        {
            Requests.SettlementFormatPrint = format;
        }

        [Given(@"I have the request data for print a settlement")]
        public void GivenIHaveTheRequestDataForPrintASettlement()
        {
            Requests.PrintSettlementRequest = new PrintSettlementRequest
            {
                Claims = CurrentData.Claims.Select(x => x.Id).ToList()
            };

            Utils.Dump("Print Settlement Request", Requests.PrintSettlementRequest);
        }


        [When(@"I make the request to print the insurer settlement")]
        public void WhenIMakeTheRequestToPrintTheInsurerSettlement()
        {
            var path = $"/insurers/{ConstantElements.InsurerId}";
            if (WithFormat)
                path = path + "/print?format=" + Requests.SettlementFormatPrint;

            var requestBody = Requests.EmptyBodyRequest ? new object() : Requests.PrintSettlementRequest;
            Requests.EmptyBodyRequest = false;
            Responses.PrintSettlementResponse = TestClients.InternalRestClient.RequestToEndpoint<PrintSettlementResponse>(Method.PUT, path, requestBody);

            Utils.Dump("Print Settlement Response", Responses.PrintSettlementResponse);
            
        }

    }
}
