namespace Dealership.ConsoleClient.Core
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Common;
    using Data;
    using Data.Seeders;
    using DataSeed.Seeders;
    using ExcelFilesProcessing;
    using ExcelReportGenerator;
    using ExcelReportGenerator.Contracts;
    using JsonReporter;
    using Models.Models.MongoDbSource;
    using Models.Models.SalesReportSource;
    using Models.Models.XmlSource;
    using MongoDb;
    using Reports.Models;
    using Reports.Models.Contracts;
    using XmlFilesProcessing.Readers;
    using XmlFilesProcessing.Writers.Common;
    using XmlFilesProcessing.Writers.Contracts;

    public class CommandProcessor
    {
        private readonly TextWriter textWriter;

        private CommandProcessor(TextWriter textWriter)
        {
            this.textWriter = textWriter;
        }

        public static CommandProcessor Create(TextWriter textWriter)
        {
            return new CommandProcessor(textWriter);
        }

        public void ExecuteCommand(Command command)
        {
            switch (command)
            {
                case Command.SeedDataFromMongo:
                    SeedDataFromMongo();
                    break;
                case Command.SeedDataFromXml:
                    SeedDataFromXml();
                    break;
                case Command.SeedDataFromSalesReports:
                    SeedDataFromSalesReports();
                    break;
                case Command.GenerateJsonReports:
                    GenerateJsonReports();
                    break;
                case Command.GenerateExcelReport:
                    GenerateExcelReport();
                    break;
                case Command.GenerateXmlShopReport:
                    GenerateXmlShopReport();
                    break;
                case Command.GenerateXmlDailyShopReport:
                    GenerateXmlDailyShopReport();
                    break;
                case Command.GenerateRdfAggregateDailySalesReport:
                    GenerateRdfAggregateDailySalesReport();
                    break;
            }
        }

        private void SeedDataFromMongo()
        {
            textWriter.WriteLine("Seeding data from Mongo...");
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

            textWriter.WriteLine("Mongo data seeded successfully!");
        }

        private void SeedDataFromXml()
        {
            textWriter.WriteLine("Seeding data from XML...");
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

            textWriter.WriteLine("XML data seeded successfully!");
        }

        private void SeedDataFromSalesReports()
        {
            textWriter.WriteLine("Unzipping MS Excel files and loading into MS SQL database....");  //TODO USE LOGGER

            ProcessZipFiles();

            textWriter.WriteLine("Unzipping and loading completed successfully!");
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
                ReportReader reportReader = new ReportReader(seedingSQLDBFromZip, data);
                reportReader.ParseExcelData(matchingDirectories);
            }
        }

        private void GenerateExcelReport()
        {
            IExcelReportGenerator excelReportGenerator = new ReportGenerator();

            string reportsPath = Constants.ExcelReportsPath;
            string excelReportName = Constants.ExcelReportName;

            textWriter.WriteLine("Generating Excel report...");

            if (File.Exists(Path.Combine(reportsPath, excelReportName)))
            {
                textWriter.WriteLine("Excel report already exists.");
            }
            else
            {
                try
                {
                    excelReportGenerator.GenerateExcelReport(reportsPath, excelReportName);
                    textWriter.WriteLine("Excel Report file created successfully!");
                }
                catch (Exception)
                {
                    textWriter.WriteLine("Error occured. Cannot create Excel report...");
                }
            }
        }

        public void GenerateJsonReports()
        {
            string directoryPath = Constants.JsonReportsPath;

            var reports = JsonReports.GenerateReports(directoryPath);
            textWriter.WriteLine("JSON Reports created!");

            JsonReports.SeedReportsToMySQL(reports);
            textWriter.WriteLine("Reports Updated to MySQL database!");
        }

        public static void GenerateXmlDailyShopReport()
        {

            var dbContext = new DealershipDbContext();
            ReportQuery query = new ReportQuery();

            ICollection<IXmlDailyShopReport> dailyReport = new List<IXmlDailyShopReport>();
            IReportWriter dailyWrite = new XmlDailyShopReportWriter(query.DailyShopReport(dbContext, dailyReport));

            dailyWrite.Write();
        }

        public void GenerateXmlShopReport()
        {
            textWriter.WriteLine("Generating Xml report...");

            var dbContext = new DealershipDbContext();
            ReportQuery query = new ReportQuery();

            ICollection<IXmlShopReport> totalReport = new List<IXmlShopReport>();
            IReportWriter totalWriter = new XmlShopReportWriter(query.ShopReport(dbContext, totalReport));

            totalWriter.Write();
            textWriter.WriteLine("Xml report created successfully!");
        }

        public void GenerateRdfAggregateDailySalesReport()
        {
            textWriter.WriteLine("Generating Pdf report...");
            var dbContext = new DealershipDbContext();
            ReportQuery query = new ReportQuery();

            ICollection<IPdfAggregatedDailySalesReport> totalReport = new List<IPdfAggregatedDailySalesReport>();
            IReportWriter totalWriter = new PdfAggregatedDailySalesReportWriter(query.AggregatedDailySalesReports(dbContext, totalReport));

            totalWriter.Write();

            textWriter.WriteLine("Pdf report created successfully!");
        }
    }
}



