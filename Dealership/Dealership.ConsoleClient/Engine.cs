using System;
using System.Linq;

using Dealership.Common;
using Dealership.Data;
using Dealership.MongoDb;
using Dealership.XmlFilesProcessing.Readers;
using Dealership.Data.Seeders;
using Dealership.DataSeed.Seeders;
using Dealership.ExcelReportGenerator;
using Dealership.ExcelReportGenerator.Contracts;

namespace Dealership.ConsoleClient
{
    public class Engine
    {
        private static readonly IExcelReportGenerator ExcelReportGenerator = new ReportGenerator();
        private static Engine instance;

        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }

                return instance;
            }
        }

        public void Start()
        {
            SeedDataFromMongo();

            SeedDataFromXml();

            //w8ing for mysql reports code...
            //WriteFromMySqlAndSqLiteToExcel();
        }

        private void SeedDataFromMongo()
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

        private void SeedDataFromXml()
        {
            var xmlEmployeeReader = new XmlEmployeeReader();

            var dbContext = new DealershipDbContext();
            var data = new DealershipData(dbContext);
            var employeeSeeder = new EmployeeSeeder(data);

            var employeeSeedUtil = new EmployeeSeedUtil(xmlEmployeeReader, employeeSeeder);

            employeeSeedUtil.Seed();
        }

        private void WriteFromMySqlAndSqLiteToExcel()
        {
            try
            {
                ExcelReportGenerator.GenerateExcelReport(selectedPathAndFileName.Item1, selectedPathAndFileName.Item2);

                Console.WriteLine("Excel Report successfully created..");
            }
            catch (Exception)
            {
                Console.WriteLine("Error! Cannot create Excel report...");
            }
        }
    }
}
