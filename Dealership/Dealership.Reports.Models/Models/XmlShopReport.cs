
using Dealership.Reports.Models.Contracts;

namespace DealerShip.Reports.Models
{
    public class XmlShopReport : IXmlShopReport
    {
        public string ShopPlace { get; set; }

        public string Location { get; set; }

        public decimal? TotalBudget { get; set; }
    }
}
