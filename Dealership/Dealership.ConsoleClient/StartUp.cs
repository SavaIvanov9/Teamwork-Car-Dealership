using System;
using System.Collections.Generic;
using System.Linq;
using Dealership.Common;
using Dealership.Data;
using Dealership.MongoDb;
using Dealership.XmlFilesProcessing.Readers;
using Dealership.Data.Seeders;
using Dealership.DataSeed.Seeders;
using Dealership.ExcelFilesProcessing;
using Dealership.Reports.Models.Contracts;
using DealerShip.Reports.Models;

namespace Dealership.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            //SeedDataFromMongo();

            //SeedDataFromXml();

            //SeedDataFromSalesReports();

            ICollection<IXmlShopReport> report = new List<IXmlShopReport>(); 

            using (var dbContext = new DealershipDbContext())
            {
                var shops = dbContext.Shops.ToList();

                foreach (var shop in shops)
                {
                    var budget = dbContext.Sales.Where(s => s.ShopId == shop.Id).Sum(t => t.Price);

                    IXmlShopReport entity = new XmlShopReport();

                    entity.ShopPlace = shop.ToString();
                    entity.Location = shop.Address.ToString();
                    entity.TotalBudget = budget;

                    report.Add(entity);
                }
            }


        }

        private static void SeedDataFromMongo()
        {
            Console.WriteLine("Seeding data from Mongo...");
            string mongoDbConnectionString = Constants.MongoDbConnectionStringLocal;
            string mongoDbDatabaseName = Constants.MongoDbDatabaseNameLocal;

            var mongoDbHandler = new MongoDbHandler(mongoDbConnectionString, mongoDbDatabaseName);
            var dealershipDbContext = new DealershipDbContext();

            if (!mongoDbHandler.IsDataSeeded(dealershipDbContext))
            {
                mongoDbHandler.SeedData(dealershipDbContext);
            }

            var data = new DealershipData(dealershipDbContext);

            Console.WriteLine("Mongo data seeded successfully!");
        }

        private static void SeedDataFromXml()
        {
            Console.WriteLine("Seeding data from XML...");
            var xmlEmployeeReader = new XmlEmployeeReader();

            var dbContext = new DealershipDbContext();
            var data = new DealershipData(dbContext);
            var employeeSeeder = new EmployeeSeeder(data);

            var employeeSeedUtil = new EmployeeSeedUtil(xmlEmployeeReader, employeeSeeder);

            employeeSeedUtil.Seed();
            Console.WriteLine("XML data seeded successfully!");
        }

        private static void SeedDataFromSalesReports()
        {
            Console.WriteLine("Unzipping MS Excel files and loading into MS SQL database....");  //TODO USE LOGGER

            ProcessZipFiles();

            Console.WriteLine("Unzipping and loading completed successfully!");
        }

        private static void ProcessZipFiles()
        {
            SeedingSQLDBFromZip s = new SeedingSQLDBFromZip();

            var processor = new ZipUnpacker();
            processor.Unpack(Constants.PathToZipFile, Constants.PathToUnzip);

            var matchingDirectories = Utility.GetDirectoriesByPattern(Constants.PathToUnzippedFiles);
            ReportReader reportReader = new ReportReader();
            reportReader.ParseExcelData(matchingDirectories);
        }

    }
}
