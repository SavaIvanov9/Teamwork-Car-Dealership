using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Json
{
    public class PdfSaleReportEntry
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public DateTime Date { get; set; }

        public string Location { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Sum { get; set; }
    }
}