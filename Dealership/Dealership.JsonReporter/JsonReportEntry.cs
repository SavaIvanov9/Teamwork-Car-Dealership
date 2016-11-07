using Newtonsoft.Json;

namespace Dealership.JsonReporter
{
    public class JsonReportEntry
    {
        [JsonProperty("product-id")]
        public int ProductId { get; set; }

        [JsonProperty("product-name")]
        public string ProductName { get; set; }

        [JsonProperty("manufacturer-name")]
        public string ManufacturerName { get; set; }

        [JsonProperty("total-quantity-sold")]
        public int TotalQuantitySold { get; set; }

        [JsonProperty("total-income")]
        public decimal TotalIncome { get; set; }
    }
}
