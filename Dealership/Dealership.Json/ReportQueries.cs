using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Data.Contracts;
using Dealership.Reports.Models;

namespace Dealership.Json
{
    class ReportQueries
    {
        private readonly IDealershipData DealershipData;

        public ReportQueries(IDealershipData DealershipData)
        {
            this.DealershipData = DealershipData;
        }

        /// <summary>
        // Query for getting all sold items information (product name, quantity, unit price, sum, date)
        /// </summary>
        //public IQueryable<PdfSaleReportEntry> GetPdfSaleReportsFromDatabase(DateTime startDate, DateTime endDate)
        //{
        //    var salesReportEntries = from sre in this.DealershipData.SalesReportEntries.All()
        //                             join pro in this.DealershipData.Products.All() on sre.ProductId equals pro.ProductId
        //                             join sl in this.DealershipData.SalesReports.All() on sre.SalesReportId equals sl.SalesReportId
        //                             where sl.ReportDate >= startDate && sl.ReportDate <= endDate
        //                             orderby sl.ReportDate
        //                             select new PdfSaleReportEntry
        //                             {
        //                                 Name = pro.Name,
        //                                 Quantity = sre.Quantity,
        //                                 Date = sl.ReportDate,
        //                                 UnitPrice = sre.UnitPrice,
        //                                 Location = sl.Store.Name,
        //                                 Sum = sre.Sum
        //                             };

        //    return salesReportEntries;
        //}

        //public IQueryable<XmlSaleReport> GetXmlSaleReportsFromDatabase(DateTime startDate, DateTime endDate)
        //{
        //    var reportData = (from m in this.DealershipData.Manufacturers.All()
        //                      join p in this.DealershipData.Products.All() on m.ManufacturerId equals p.ManufacturerId
        //                      join s in this.DealershipData.SalesReportEntries.All() on p.ProductId equals s.ProductId
        //                      join l in this.DealershipData.SalesReports.All() on s.SalesReportId equals l.SalesReportId
        //                      where l.ReportDate >= startDate && l.ReportDate <= endDate
        //                      select new XmlSaleReport()
        //                      {
        //                          ManufacturerName = m.Name,
        //                          ReportDate = l.ReportDate,
        //                          Sum = s.Sum
        //                      });

        //    return reportData;
        //}

        public IQueryable<JsonProductsReportEntry> GetJsonProductsReportsFromDatabase()
        {
            var productsReportsEntries = from vehicle in this.DealershipData.Vehicles.All()
                                         join brand in this.DealershipData.Brands.All() on vehicle.BrandId equals brand.ManufacturerId
                                         join sre in this.DealershipData.SalesReportEntries.All() on vehicle.ProductId equals sre.ProductId
                                         group sre by new { ProductId = sre.ProductId, ProductName = vehicle.Name, ManufacturerName = brand.Name }
                                         into grp
                                         select new JsonProductsReportEntry()
                                         {
                                             ProductId = grp.Key.ProductId,
                                             ProductName = grp.Key.ProductName,
                                             ManufacturerName = grp.Key.ManufacturerName,
                                             TotalQuantitySold = grp.Sum(x => x.Quantity),
                                             TotalIncome = grp.Sum(x => x.Sum)
                                         };

            return productsReportsEntries;
        }
    }
}