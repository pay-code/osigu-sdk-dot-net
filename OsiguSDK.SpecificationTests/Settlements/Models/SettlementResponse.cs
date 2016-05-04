using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsiguSDK.SpecificationTests.Settlements.Models
{

    public class SettlementResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "total_amount")]
        public decimal TotalAmount { get; set; }

        [JsonProperty(PropertyName = "total_discount")]
        public decimal TotalDiscount { get; set; }

        [JsonProperty(PropertyName = "date_from")]
        public DateTime DateFrom { get; set; }

        [JsonProperty(PropertyName = "date_to")]
        public DateTime DateTo { get; set; }

        [JsonProperty(PropertyName = "currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty(PropertyName = "revenue_share")]
        public decimal RevenueShare { get; set; }

        [JsonProperty(PropertyName = "insurer_id")]
        public int InsurerId { get; set; }

        [JsonProperty(PropertyName = "provider_id")]
        public int ProviderId { get; set; }

        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "taxes")]
        public List<Taxes> Taxes { get; set; }

        [JsonProperty(PropertyName = "items")]
        public List<SettlementItemResponse> Items { get; set; }

        [JsonProperty(PropertyName = "commissions")]
        public List<Commission> Comissions { get; set; }

        [JsonProperty(PropertyName = "retentions")]
        public List<Retentions> Retentions { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public SettlemetSelfLinksResponse Links { get; set; }

        /*[JsonProperty(PropertyName = "provider_company_id")]
       public int ProviderCompanyId { get; set; }*/

        /* [JsonProperty(PropertyName = "mailed_at")]
         public DateTime MailedAt { get; set; }*/

        /*[JsonProperty(PropertyName = "payments")]
        public List<Payment> Payments { get; set; }*/
    }

    public class SettlemetSelfLinksResponse
    {
        [JsonProperty(PropertyName = "self")]
        public SettlemetHrefResponse Self { get; set; }
    }

    public class SettlemetHrefResponse
    {
        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }
    }
}