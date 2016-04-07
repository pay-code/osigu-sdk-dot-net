namespace OsiguSDK.SpecificationTests.AuxiliarModels
{
    public class SettlementResponse
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalDiscounts { get; set; }
        public int InsurerId { get; set; }
        public int ProviderId { get; set; }
        public int ProviderCompanyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal RevenueShare { get; set; }
    }
}