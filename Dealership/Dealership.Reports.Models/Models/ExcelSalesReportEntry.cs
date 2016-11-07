using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
