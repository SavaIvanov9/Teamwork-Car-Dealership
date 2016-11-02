namespace Dealership.Common
{
    public static class Constants
    {
        public const string MongoDbConnectionStringLocal = "mongodb://localhost";
        public const string MongoDbDatabaseNameLocal = "Products";
        public const string MongoDbConnectionString = "mongodb://admin:123456@ds061246.mlab.com:61246/teamwork?connect=replicaSet";
        public const string MongoDbDatabaseName = "teamwork";


        public const string AllowedExcelFileExtensionPattern = "*.xls";
        public const string PathToUnzip = "../../../Data";
        public const string PathToUnzippedFiles = "../../../Data/Sample-Sales-Reports";

        public const string PathToZipFile = "../../../Sample-Sales-Reports.zip";

        //Connection constants - they should be moved to configuration files
        public const string ExcelSalesReportsConnectionStringPattern = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                                @"Data Source= {0};Extended Properties='Excel 12.0 Xml;HDR=YES;'";

        public const string ExcelSalesReportsConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                                    "Extended Properties='Excel 12.0 Xml;HDR=YES;'";//Coupling with expected datasource

        public const string ExcelReportName = @"excel-report.xlsx";
        public const string ExtractedExcelReportsPath = @"../../../../Reports/Excel_Reports/";
    }
}
