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
    public class XmlQuery
    {
        public XmlQuery()
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
                            market.Transactions = new Dictionary<DateTime?, List<decimal?>>();
                            market.Transactions[day.Key.DateOfSale] = new List<decimal?>();
                        }

                        foreach (var sale in day)
                        {
                            market.Transactions[day.Key.DateOfSale].Add(sale.Price);
                        }
                    }

                    report.Add(market);
                }
            }

            return report;
        }
    }
}
