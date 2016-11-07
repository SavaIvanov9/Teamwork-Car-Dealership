using Dealership.Reports.Models;
using System;
using System.Collections.Generic;

namespace DealerShip.Reports.Models
{
    public class ExcelSalesReport
    {
        public DateTime DateOfSale { get; set; }

        public string Location { get; set; }

        public string DistributorName { get; set; }

        public ICollection<ExcelSalesReportEntry> Records { get; set; }

        public ExcelSalesReport()
        {
            this.Records = new List<ExcelSalesReportEntry>();
        }
    }
}
