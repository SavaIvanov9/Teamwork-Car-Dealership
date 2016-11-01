using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dealership.Reports.Models
{
    
    public class JsonProductsReportEntry
    {
        [JsonProperty("product-id")]
        public int ProductId { get; set; }

        [JsonProperty("product-name")]
        public string ProductName { get; set; }

        [JsonProperty("manufacturer-name")]
        public string ManufacturerName { get; set; }

        [JsonProperty("total-quantity-sold")]
        public decimal TotalQuantitySold { get; set; }

        [JsonProperty("total-income")]
        public decimal TotalIncome { get; set; }
    }
}
