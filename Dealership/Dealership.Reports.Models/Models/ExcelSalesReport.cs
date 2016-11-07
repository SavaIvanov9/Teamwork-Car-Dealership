using System;
using System.Collections.Generic;

namespace Dealership.Reports.Models
{
    public class ExcelSalesReport
    {
        public DateTime DateOfSale { get; set; }

        public string DistributorName { get; set; }

        public ICollection<ExcelSalesReportEntry> Records { get; set; }

        public ExcelSalesReport()
        {
            this.Records = new List<ExcelSalesReportEntry>();
        }
    }
}
