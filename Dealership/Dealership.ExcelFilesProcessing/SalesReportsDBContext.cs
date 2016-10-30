using Dealership.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.ExcelFilesProcessing.Models;
using Dealership.Data.Migrations;

namespace Dealership.ExcelFilesProcessing
{
    public class SalesReportsDBContext : DbContext
    {
        public SalesReportsDBContext()
            : base("name=DealershipDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SalesReportsDBContext, Configuration1>());
        }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Sale> Sales { get; set; }

        public IDbSet<Shop> Shops { get; set; }  //TODO when the two shops tables converge, remove lower dash

    }
}
