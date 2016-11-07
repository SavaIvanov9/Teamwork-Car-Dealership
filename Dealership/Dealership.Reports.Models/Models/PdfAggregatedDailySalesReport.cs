using System;
using System.Collections.Generic;
using Dealership.Reports.Models.Contracts;

namespace Dealership.Reports.Models.Models
{
    public class PdfAggregatedDailySalesReport : IPdfAggregatedDailySalesReport
    {
        private DateTime? date;
        private ICollection<IPdfAggregatedDailyEntity> dailyEntities;

        public PdfAggregatedDailySalesReport(DateTime date)
        {
            this.Date = date;
            this.DailyEntities = new HashSet<IPdfAggregatedDailyEntity>();
        }

        public DateTime? Date
        {
            get
            {
                return this.date;
            }

            set
            {
                this.date = value;
            }
        }

        public decimal? TotalDailySales { get; set; }

        public ICollection<IPdfAggregatedDailyEntity> DailyEntities
        {
            get
            {
                return this.dailyEntities;
            }

            set
            {
                this.dailyEntities = value;
            }
        }

    }
}
