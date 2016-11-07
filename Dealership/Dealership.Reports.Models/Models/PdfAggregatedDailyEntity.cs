using Dealership.Reports.Models.Contracts;

namespace Dealership.Reports.Models.Models
{
    public class PdfAggregatedDailyEntity : IPdfAggregatedDailyEntity
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
