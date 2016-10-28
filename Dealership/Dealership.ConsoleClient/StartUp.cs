using System;
using System.Linq;

using Dealership.Common;
using Dealership.Data;
using Dealership.MongoDb;

namespace Dealership.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            string mongoDbConnectionString = Constants.MongoDbConnectionStringLocal;
            string mongoDbDatabaseName = Constants.MongoDbDatabaseNameLocal;

            var mongoDbHandler = new MongoDbHandler(mongoDbConnectionString, mongoDbDatabaseName);
            var dealershipDbContext = new DealershipDbContext();

            if (!mongoDbHandler.IsDataSeeded(dealershipDbContext))
            {
                mongoDbHandler.SeedData(dealershipDbContext);
            }

            var data = new DealershipData(dealershipDbContext);

            Console.WriteLine(data.Vehicles.All().Count());
            Console.WriteLine(data.Brands.All().FirstOrDefault().Vehicles.Count);
        }
    }
}
