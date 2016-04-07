using System;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class Item
    {
        public int Id { get; set; }
        public decimal ClaimAmount { get; set; }
        public Claim Claim { get; set; }
        public DateTime CreatedAt { get; set; } 
    }
}