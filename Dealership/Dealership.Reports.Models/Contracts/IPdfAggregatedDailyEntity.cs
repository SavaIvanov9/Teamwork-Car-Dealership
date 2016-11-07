using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Reports.Models.Contracts
{
    public interface IPdfAggregatedDailyEntity
    {
        string Brand { get; set; }

        string Model { get; set; }

        int? Quantity { get; set; }
                                                                                                
        decimal? UnitPrice { get; set; }

        decimal? TotalPrice { get; set; }
    }
}
