using Dealership.Common;
using Dealership.Data;
using System.Collections.Generic;
using System.IO;

namespace Dealership.ExcelFilesProcessing
{
    public class ReportReader
    {
        private readonly SeedingSQLDBFromZip seedingSQLDBFromZip;

        public ReportReader(SeedingSQLDBFromZip seedingSQLDBFromZip)
        {
            this.seedingSQLDBFromZip = seedingSQLDBFromZip;
        }

        public void ParseExcelData(IEnumerable<DirectoryInfo> matchingDirectories)
        {
            var excelSaleReportReader = new SalesReportsReaderExcel(Constants.ExcelSalesReportsConnectionStringPattern);

            using (var dealershipDbContext = new DealershipDbContext())
            {
                var result = dealershipDbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE [Sales]");
            }

            foreach (var dir in matchingDirectories)
            {
                foreach (var excelFile in dir.GetFiles(Constants.AllowedExcelFileExtensionPattern))
                {
                    var excelData = excelSaleReportReader.ReadReport(excelFile.FullName, dir.Name);
                    this.seedingSQLDBFromZip.SeedSalesTable(excelData);
                }
            }
        }
    }
}
