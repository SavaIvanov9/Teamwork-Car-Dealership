using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dealership.Data;
using Dealership.Models.Models.XmlSource;
using Dealership.Reports.Models.Contracts;
using Dealership.Reports.Models.Models;
using Dealership.XmlFilesProcessing.Writers.Common;
using Dealership.XmlFilesProcessing.Writers.Contracts;
using DealerShip.Reports.Models;

namespace Dealership.Reports.Models
{
    public class ReportQuery
    {
        public ReportQuery()
        {
        }

        public ICollection<IXmlShopReport> ShopReport(DealershipDbContext dbContext, ICollection<IXmlShopReport> report)
        {
            using (dbContext)
            {
                var shops = dbContext.Shops.ToList();

                foreach (var shop in shops)
                {
                    var budget = dbContext.Sales.Where(s => s.ShopId == shop.Id).Sum(t => t.Price);

                    IXmlShopReport entity = new XmlShopReport();

                    entity.ShopPlace = shop.Name;
                    entity.Location = shop.Address.Street;
                    entity.TotalBudget = budget;

                    report.Add(entity);
                }
            }

            return report;
        }

        public ICollection<IXmlDailyShopReport> DailyShopReport(DealershipDbContext dbContext, ICollection<IXmlDailyShopReport> report)
        {
            using (dbContext)
            {
                var shops = dbContext.Shops.ToList();

                foreach (var shop in shops)
                {
                    var dailySells = dbContext.Sales.Where(s => s.ShopId == shop.Id).GroupBy(s => new { s.DateOfSale, s.Price}).OrderBy(d => d.Key.DateOfSale);


                    IXmlDailyShopReport market = new XmlDailyShopReport();

                    market.ShopPlace = shop.Name;

                    foreach (var day in dailySells)
                    {
                        if (!market.Transactions.ContainsKey(day.Key.DateOfSale))
                        {
                            market.Transactions[day.Key.DateOfSale] = new List<decimal?>();
                        }

                        market.Transactions[day.Key.DateOfSale].Add(day.Key.Price);
                    }

                    report.Add(market);
                }
            }

            return report;
        }

        public ICollection<IPdfAggregatedDailySalesReport> AggregatedDailySalesReports(DealershipDbContext dbContext,
            ICollection<IPdfAggregatedDailySalesReport> report)
        {
            using (dbContext)
            {
                var query = dbContext.Sales
                    .Join(dbContext.Vehicles,
                        sales => sales.VehicleId,
                        vehicle => vehicle.Id,
                        (sale, vehicle) => new { sale, vehicle })
                    .Join(dbContext.Brands,
                        salesVehicle => salesVehicle.vehicle.BrandId,
                        brand => brand.Id,
                        (salesVehicle, brand) =>
                            new { salesVehicle, brand })
                    .Select(all =>
                       new
                       {
                           DateOfSale = all.salesVehicle.sale.DateOfSale,
                           Brand = all.salesVehicle.vehicle.Brand.Name,
                           Model = all.salesVehicle.vehicle.Model,
                           Quantity = all.salesVehicle.sale.Quantity,
                           UnitPrice = all.salesVehicle.sale.Price / all.salesVehicle.sale.Quantity,
                           TotalPrice = all.salesVehicle.sale.Price,
                       })
                        .GroupBy(s => s.DateOfSale);

                foreach (var day in query)
                {
                    IPdfAggregatedDailySalesReport dayReport = new PdfAggregatedDailySalesReport(day.Key.Value);

                    foreach (var entity in day)
                    {
                        IPdfAggregatedDailyEntity dailyEntity = new PdfAggregatedDailyEntity();

                        dailyEntity.Brand = entity.Brand;
                        dailyEntity.Model = entity.Model;
                        dailyEntity.Quantity = entity.Quantity;
                        dailyEntity.UnitPrice = entity.UnitPrice;
                        dailyEntity.TotalPrice = entity.TotalPrice;

                        dayReport.DailyEntities.Add(dailyEntity);
                    }

                    decimal? totalDailySales = day.Sum(s => s.TotalPrice);
                    dayReport.TotalDailySales = totalDailySales;

                    report.Add(dayReport);
                }
            }

            return report;
        }
    }
}
