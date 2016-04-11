using System;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class TaxRetentions
    {
        public int Id { get; set; }
        public decimal Percentage { get; set; }
        public decimal Amount { get; set; }
        public Tax TaxRetention { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}