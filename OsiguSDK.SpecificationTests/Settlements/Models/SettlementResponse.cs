using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{
    public class SettlementResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty(PropertyName = "total_discount_amount")]
        public decimal TotalDiscounts { get; set; }

        [JsonProperty(PropertyName = "insurer_id")]
        public int InsurerId { get; set; }

        [JsonProperty(PropertyName = "provider_id")]
        public int ProviderId { get; set; }

        [JsonProperty(PropertyName = "provider_company_id")]
        public int ProviderCompanyId { get; set; }

        [JsonProperty(PropertyName = "currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty(PropertyName = "revenue_share")]
        public decimal RevenueShare { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "date_from")]
        public DateTime DateFrom { get; set; }

        [JsonProperty(PropertyName = "date_to")]
        public DateTime DateTo { get; set; }

        [JsonProperty(PropertyName = "mailed_at")]
        public DateTime MailedAt { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "commissions")]
        public List<Comission> Comissions { get; set; }

        [JsonProperty(PropertyName = "taxes")]
        public List<Taxes> Taxes { get; set; }

        [JsonProperty(PropertyName = "payments")]
        public List<Payment> Payments { get; set; }
        
        [JsonProperty(PropertyName = "tax_retentions")]
        public List<TaxRetentions> TaxRetentions { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<SettlementItemResponse> Items { get; set; } 
    }
}