using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Reports.Models.Contracts
{
    public interface IXmlDailyShopReport
    {
        string ShopPlace { get; set; }

        //ICollection<DateTime?> DateOfSale { get; set; }

        //ICollection<decimal?> Price { get; set; }

        IDictionary<DateTime?, List<decimal?>> Transactions { get; set; }
    }
}
