namespace Dealership.Models.Models.JsonReports
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;

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