using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerShip.Models
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
