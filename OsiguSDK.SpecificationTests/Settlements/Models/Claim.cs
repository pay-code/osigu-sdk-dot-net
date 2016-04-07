namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string DocumentNumber { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; } 
    }
}