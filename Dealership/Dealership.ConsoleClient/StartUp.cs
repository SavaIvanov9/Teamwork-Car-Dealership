using System;
using System.Linq;

using Dealership.Common;
using Dealership.Data;
using Dealership.MongoDb;
using Dealership.XmlFilesProcessing.Readers;

namespace Dealership.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            SeedDataFromMongo();

            SeedDataFromXml();
        }

        private static void SeedDataFromMongo()
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

        private static void SeedDataFromXml()
        {
            var xmlDataReader = new XmlEmployeeReader();

            var employees = xmlDataReader.ReadEmployees();

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.FirstName);
            }
        }
    }
}
