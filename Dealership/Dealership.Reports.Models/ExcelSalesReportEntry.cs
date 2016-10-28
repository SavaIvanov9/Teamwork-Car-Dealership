using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealerShip.Models
{
    public class ExcelSalesReportEntry
    {
        public string Distributor { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public decimal UnitPrice { get; set; }

    }
}

