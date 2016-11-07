using System;
using System.Collections.Generic;
using Dealership.Common;
using Dealership.Data;
using Dealership.MongoDb;
using Dealership.XmlFilesProcessing.Readers;
using Dealership.Data.Seeders;
using Dealership.DataSeed.Seeders;
using Dealership.ExcelFilesProcessing;
using Dealership.ExcelReportGenerator;
using Dealership.ExcelReportGenerator.Contracts;
using Dealership.Reports.Models;
using Dealership.Reports.Models.Contracts;
using Dealership.XmlFilesProcessing.Writers.Common;
using Dealership.XmlFilesProcessing.Writers.Contracts;
using Dealership.Models.Models.XmlSource;
using Dealership.Models.Models.SalesReportSource;
using Dealership.Models.Models.MongoDbSource;
using Dealership.JsonReporter;

namespace Dealership.ConsoleClient
{
    public class StartUp
    {
        public static void Main()
        {
            SeedDataFromMongo();

            SeedDataFromXml();

            SeedDataFromSalesReports();

            GenerateXmlShopReport();

            GenerateXmlDailyShopReport();

            GenerateRdfAggregateDailySalesReport();

            GenerateJsonReports();

            GenerateExcelReport();
        }

        public static void GenerateXmlDailyShopReport()
        {

            var dbContext = new DealershipDbContext();
            ReportQuery query = new ReportQuery();

            ICollection<IXmlDailyShopReport> dailyReport = new List<IXmlDailyShopReport>();
            IReportWriter dailyWrite = new XmlDailyShopReportWriter(query.DailyShopReport(dbContext, dailyReport));

            dailyWrite.Write();
        }

        public static void GenerateXmlShopReport()
        {
            var dbContext = new DealershipDbContext();
            ReportQuery query = new ReportQuery();

            ICollection<IXmlShopReport> totalReport = new List<IXmlShopReport>();
            IReportWriter totalWriter = new XmlShopReportWriter(query.ShopReport(dbContext, totalReport));

            totalWriter.Write();
        }

        public static void GenerateRdfAggregateDailySalesReport()
        {
            var dbContext = new DealershipDbContext();
            ReportQuery query = new ReportQuery();

            ICollection<IPdfAggregatedDailySalesReport> totalReport = new List<IPdfAggregatedDailySalesReport>();
            IReportWriter totalWriter = new PdfAggregatedDailySalesReportWriter(query.AggregatedDailySalesReports(dbContext, totalReport));

            totalWriter.Write();
        }


        private static void SeedDataFromMongo()
        {
            Console.WriteLine("Seeding data from Mongo...");
            string mongoDbConnectionString = Constants.MongoDbConnectionStringLocal;
            string mongoDbDatabaseName = Constants.MongoDbDatabaseNameLocal;

            using (var dbContext = new DealershipDbContext())
            {
                var data = new DealershipData(dbContext);

                var vehicles = new DealershipRepository<Vehicle>(dbContext);
                var brands = new DealershipRepository<Brand>(dbContext);
                var fuels = new DealershipRepository<Fuel>(dbContext);
                var vehicleTypes = new DealershipRepository<VehicleType>(dbContext);

                var mongoDbSeeder = new MongoDbSeeder(
                    mongoDbConnectionString,
                    mongoDbDatabaseName,
                    data,
                    vehicles,
                    brands,
                    fuels,
                    vehicleTypes
                    );
                if (!mongoDbSeeder.IsDataSeeded())
                {
                    mongoDbSeeder.SeedData();
                }
            }

            Console.WriteLine("Mongo data seeded successfully!");
        }

        private static void SeedDataFromXml()
        {
            Console.WriteLine("Seeding data from XML...");
            var xmlEmployeeReader = new XmlEmployeeReader();

            using (var dbContext = new DealershipDbContext())
            {
                var data = new DealershipData(dbContext);

                var employees = new DealershipRepository<Employee>(dbContext);
                var positions = new DealershipRepository<Position>(dbContext);
                var countries = new DealershipRepository<Country>(dbContext);
                var cities = new DealershipRepository<City>(dbContext);
                var addresses = new DealershipRepository<Address>(dbContext);
                var shops = new DealershipRepository<Shop>(dbContext);

                var employeeSeeder = new EmployeeSeeder(data, employees, positions, countries, cities, addresses, shops);

                var employeeSeedUtil = new EmployeeSeedUtil(xmlEmployeeReader, employeeSeeder);

                employeeSeedUtil.Seed();
            }

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
            using (var dbContext = new DealershipDbContext())
            {
                var data = new DealershipData(dbContext);
                var employees = new DealershipRepository<Employee>(dbContext);
                var sales = new DealershipRepository<Sale>(dbContext);
                var vehicles = new DealershipRepository<Vehicle>(dbContext);
                var shops = new DealershipRepository<Shop>(dbContext);

                SeedingSQLDBFromZip seedingSQLDBFromZip = new SeedingSQLDBFromZip(data, employees, shops, sales, vehicles);

                var processor = new ZipUnpacker();
                processor.Unpack(Constants.PathToZipFile, Constants.PathToUnzip);

                var matchingDirectories = Utility.GetDirectoriesByPattern(Constants.PathToUnzippedFiles);
                ReportReader reportReader = new ReportReader(seedingSQLDBFromZip);
                reportReader.ParseExcelData(matchingDirectories);
            }
        }

        private static void GenerateExcelReport()
        {
            IExcelReportGenerator excelReportGenerator = new ReportGenerator();

            string reportsPath = Constants.ExcelReportsPath;
            string excelReportName = Constants.ExcelReportName;

            try
            {
                excelReportGenerator.GenerateExcelReport(reportsPath, excelReportName);
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured. Cannot create Excel report...");
            }
        }

        public static void GenerateJsonReports()
        {
            string directoryPath = Constants.JsonReportsPath;

            var reports = JsonReports.GenerateReports(directoryPath);
            Console.WriteLine("JSON Reports created!");

            JsonReports.SeedReportsToMySQL(reports);
            Console.WriteLine("Reports Updated to MySQL database!");
        }
    }
}
