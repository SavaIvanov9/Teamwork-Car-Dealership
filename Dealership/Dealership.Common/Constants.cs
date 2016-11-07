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
        public const string PathToUnzippedFiles = "../../../Data/Sales-Reports";

        public const string PathToZipFile = "../../../../Sales-Reports.zip";

        public const string ExcelSalesReportsConnectionStringPattern = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                                @"Data Source= {0};Extended Properties='Excel 12.0 Xml;HDR=YES;'";

        public const string ExcelReportName = @"excel-report.xlsx";
        public const string ExcelReportsPath = @"../../../../Reports/Excel_Reports/";

        public const string JsonReportsPath = "../../../Reports/Json-Reports";

        public const string XmlReportsPath = "../../../../Reports/Xml-Reports";

        public const string PdfReportsPath = "../../../../Reports/Rdf-Reports";

        public static string ToCurrency(this decimal text)
        {
            return string.Format("{0:C}", text);
        }
    }
}
