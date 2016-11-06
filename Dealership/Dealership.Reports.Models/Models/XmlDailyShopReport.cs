using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Reports.Models.Contracts;

namespace Dealership.Reports.Models.Models
{
    public class XmlDailyShopReport : IXmlDailyShopReport
    {
        private IDictionary<DateTime?, List<decimal?>> transactions;

        public XmlDailyShopReport()
        {
            this.transactions = new Dictionary<DateTime?, List<decimal?>>();
        }
        public string ShopPlace { get; set; }

        //public ICollection<DateTime?> DateOfSale { get; set; }

        //public ICollection<decimal?> Price { get; set; }

        public IDictionary<DateTime?, List<decimal?>> Transactions
        {
            get
            {
                return this.transactions;
            }

            set
            {
                this.transactions = value;
            }
        }
    }
}
