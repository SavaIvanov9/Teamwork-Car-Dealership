using System;
using System.Collections.Generic;

namespace Dealership.Reports.Models.Contracts
{
    public interface IPdfAggregatedDailySalesReport
    {
        DateTime? Date { get; set; }

        decimal? TotalDailySales { get; set; }

        ICollection<IPdfAggregatedDailyEntity> DailyEntities { get; set; }
    }
}