namespace Dealership.Reports.Models
{
    public class ExcelSalesReportEntry
    {
        public string VehicleModel { get; set; }

        public int EmployeeId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

    }
}
