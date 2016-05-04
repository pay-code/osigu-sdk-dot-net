using System.Collections.Generic;
using OsiguSDK.SpecificationTests.Settlements.Models;

namespace OsiguSDK.SpecificationTests.Settlements.Calculation
{
    public interface ISettlementCalculator
    {
        decimal GetTotalAmount();
        decimal GetTotalDiscount();
        List<Taxes> GetTaxes();
        List<Commission> GetCommissions();
        List<Retentions> GetRetentions();
    }
    
}