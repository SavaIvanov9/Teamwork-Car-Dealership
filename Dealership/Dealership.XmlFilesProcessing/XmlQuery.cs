using System.Collections.Generic;
using System.Linq;
using Dealership.Data;
using Dealership.Reports.Models.Contracts;
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
    }
}
