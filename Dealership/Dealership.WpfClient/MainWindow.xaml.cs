using System.Windows;

using Dealership.Common;
using Dealership.Data;
using Dealership.Data.Seeders;
using Dealership.DataSeed.Seeders;
using Dealership.ExcelFilesProcessing;
using Dealership.MongoDb;
using Dealership.XmlFilesProcessing.Readers;

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

            var mongoDbHandler = new MongoDbHandler(mongoDbConnectionString, mongoDbDatabaseName);
            var dealershipDbContext = new DealershipDbContext();
            
            if (!mongoDbHandler.IsDataSeeded(dealershipDbContext))
            {
                mongoDbHandler.SeedData(dealershipDbContext);
            }

            // MessageBox.Show("Data from MongoDb loaded successfully!");
        }

        private void SeedDataFromXml()
        {
            var xmlEmployeeReader = new XmlEmployeeReader();

            var dbContext = new DealershipDbContext();
            var data = new DealershipData(dbContext);
            var employeeSeeder = new EmployeeSeeder(data);

            var employeeSeedUtil = new EmployeeSeedUtil(xmlEmployeeReader, employeeSeeder);

            employeeSeedUtil.Seed();

            // MessageBox.Show("Data from XML loaded successfully!");
        }

        private void SeedDataFromSalesReports()
        {
            SeedingSQLDBFromZip s = new SeedingSQLDBFromZip();

            var processor = new ZipUnpacker();
            processor.Unpack(Constants.PathToZipFile, Constants.PathToUnzip);

            var matchingDirectories = Utility.GetDirectoriesByPattern(Constants.PathToUnzippedFiles);
            ReportReader reportReader = new ReportReader();
            reportReader.ParseExcelData(matchingDirectories);

            // MessageBox.Show("Data from Excell loaded successfully!");
        }
    }
}
