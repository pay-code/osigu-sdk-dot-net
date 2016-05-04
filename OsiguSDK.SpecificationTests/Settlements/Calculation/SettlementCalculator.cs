using System;
using System.Collections.Generic;
using System.Linq;
using OsiguSDK.SpecificationTests.Settlements.Models;
using OsiguSDK.SpecificationTests.Tools;
using Claim = OsiguSDK.Providers.Models.Claim;

namespace OsiguSDK.SpecificationTests.Settlements.Calculation
{
    public class SettlementCalculator : ISettlementCalculator
    {
        private const decimal PercentageNormalCommission = 0.1m;
        private const decimal PercentageCashoutCommission = 0.05m;
        private const decimal Iva = 0.12m;
        private const decimal IvaRetention = 0.15m;
        
        public bool IsAgentRetention { get; set; }
        public List<Claim> Claims { get; set; }
        public SettlementType SettlementType { get; set; }

        private List<DataRetention> DataRetentions { get; set; }

        public SettlementCalculator(bool isAgentRetention, List<Claim> claims, SettlementType settlementType)
        {
            Claims = claims;
            IsAgentRetention = isAgentRetention;
            SettlementType = settlementType;
            CalculateDataRetention();
        }

        public decimal GetTotalAmount()
        {
            return Claims.Sum(x => x.Invoice.Amount);
        }

        public decimal GetTotalDiscount()
        {
            return SettlementType == SettlementType.Cashout
                ? DataRetentions.Sum(x => x.NormalCommission) + DataRetentions.Sum(x => x.CashoutCommission)
                : DataRetentions.Sum(x => x.NormalCommission);
        }

        public List<Taxes> GetTaxes()
        {
            var taxes = new List<Taxes>();

            var totalNormalCommision = DataRetentions.Sum(x => x.NormalCommission);
            var totalCashoutCommision = DataRetentions.Sum(x => x.CashoutCommission);
            var normalTaxeableAmount = Math.Round(totalNormalCommision/1.12m,4);
            var cashoutTaxeableAmount = Math.Round(totalCashoutCommision/1.12m,4);

            taxes.Add(new Taxes
            {
                Amount = Math.Round(totalNormalCommision - normalTaxeableAmount,4),
                Percentage = Iva,
                CreatedAt = DateTime.Now
            });

            if (SettlementType == SettlementType.Cashout)
            {
                taxes.Add(new Taxes
                {
                    Amount = Math.Round(totalCashoutCommision - cashoutTaxeableAmount,4),
                    Percentage = Iva,
                    CreatedAt = DateTime.Now
                });
            }

            return taxes;
        }

        public List<Commission> GetCommissions()
        {
            var commissions = new List<Commission>
            {
                new Commission
                {
                    Amount = Math.Round(DataRetentions.Sum(x => x.NormalCommission),4),
                    ComissionType = CommissionType.NORMAL,
                    //TODO: Percentage should be return raw
                    Percentage = PercentageNormalCommission*100,
                    CreatedAt = DateTime.Now
                }
            };

            if (SettlementType == SettlementType.Cashout)
            {
                commissions.Add(new Commission
                {
                    Amount = Math.Round(DataRetentions.Sum(x => x.CashoutCommission),4),
                    ComissionType = CommissionType.FAST_PAYMENT,
                    //TODO: Percentage should be return raw
                    Percentage = PercentageCashoutCommission*100,
                    CreatedAt = DateTime.Now
                });
            }

            return commissions;
            
        }

        public List<Retentions> GetRetentions()
        {
            var retentions = new List<Retentions>();

            if (IsAgentRetention || SettlementType == SettlementType.Normal )
                return retentions;

            retentions.Add(new Retentions
            {
                Amount = Math.Round(DataRetentions.Sum(x => x.IvaRetention),4),
                Percentage = IvaRetention,
                CreatedAt = DateTime.Now
            });

            var totalRetentionAmount = DataRetentions.Sum(x => x.RetentionBefore30000) +
                                       DataRetentions.Sum(x => x.RetentionAfter30000);

            if (totalRetentionAmount > 0)
            {
                retentions.Add(new Retentions
                {
                    Amount = Math.Round(totalRetentionAmount,4),
                    Percentage = IvaRetention,
                    CreatedAt = DateTime.Now
                });
            }

            return retentions;
        }

        private void CalculateDataRetention()
        {
            DataRetentions = new List<DataRetention>();
            foreach (var dataRetention in Claims.Select(claim => new DataRetention
            {
                ClaimId = claim.Id,
                ClaimAmount = claim.Invoice.Amount
            }))
            {
                dataRetention.NormalCommission = Math.Round(dataRetention.ClaimAmount * PercentageNormalCommission,4);
                dataRetention.CashoutCommission = Math.Round(dataRetention.ClaimAmount * PercentageCashoutCommission,4);
                dataRetention.TaxebleAmount = Math.Round(dataRetention.ClaimAmount / (1 + Iva),4);
                dataRetention.Taxes = Math.Round(dataRetention.ClaimAmount - dataRetention.TaxebleAmount,4);
                dataRetention.IvaRetention = IsAgentRetention ? 0 : Math.Round(dataRetention.Taxes * IvaRetention,4);
                dataRetention.RetentionBefore30000 = IsAgentRetention ? 0 : Math.Round(GetRetentionBefore30000(dataRetention.TaxebleAmount),4);
                dataRetention.RetentionAfter30000 = IsAgentRetention
                    ? 0
                    : Math.Round(GetRetentionAfter30000(dataRetention.RetentionBefore30000, dataRetention.TaxebleAmount),4);

                DataRetentions.Add(dataRetention);
            }
        }

        private decimal GetRetentionAfter30000(decimal retentionBefore30000, decimal taxebleAmount)
        {
            return retentionBefore30000 >= 1500 ? (taxebleAmount - 30000) * 0.07m : 0;

        }

        private decimal GetRetentionBefore30000(decimal taxebleAmount)
        {
            if (taxebleAmount > 2500 && taxebleAmount <= 30000)
                return taxebleAmount * 0.05m;

            return taxebleAmount > 30000 ? 1500 : 0;
        }

    }

    public class DataRetention
    {
        public int ClaimId { get; set; }
        public decimal ClaimAmount { get; set; }
        public decimal NormalCommission { get; set; }

        public decimal CashoutCommission { get; set; }

        public decimal TaxebleAmount { get; set; }

        public decimal Taxes { get; set; }

        public decimal IvaRetention { get; set; }

        public decimal RetentionBefore30000 { get; set; }

        public decimal RetentionAfter30000 { get; set; }

    }
}