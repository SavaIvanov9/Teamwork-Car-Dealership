using System;
using System.Windows;

using Dealership.Common;
using Dealership.Data;
using Dealership.Data.Seeders;
using Dealership.DataSeed.Seeders;
using Dealership.ExcelFilesProcessing;
using Dealership.MongoDb;
using Dealership.XmlFilesProcessing.Readers;
using Dealership.Models.Models.XmlSource;
using Dealership.Models.Models.SalesReportSource;
using Dealership.Models.Models.MongoDbSource;

namespace Dealership.WpfClient
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void BtnLoadSqlClick(object sender, RoutedEventArgs e)
        {
            this.SeedDataFromMongo();

            this.SeedDataFromXml();

            this.SeedDataFromSalesReports();

            MessageBox.Show("Data successfully loaded into SQL!");

            this.BtnXmlReports.Visibility = Visibility.Visible;
        }

        private void BtnXmlReportsClick(object sender, RoutedEventArgs e)
        {
            //Generate XML Reports
        }

        private void SeedDataFromMongo()
        {
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

            // MessageBox.Show("Data from MongoDb loaded successfully!");
        }

        private void SeedDataFromXml()
        {
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

            // MessageBox.Show("Data from XML loaded successfully!");
        }


        private void SeedDataFromSalesReports()
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

            // MessageBox.Show("Data from Excel loaded successfully!");
            }
        }
    }
}
