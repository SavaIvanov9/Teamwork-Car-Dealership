using Dealership.Common;
using Dealership.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.ExcelFilesProcessing
{
    public class ReportReader
    {
        public void ParseExcelData(IEnumerable<DirectoryInfo> matchingDirectories)
        {
            SeedingSQLDBFromZip seedingSQLDBFromZip = new SeedingSQLDBFromZip();
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
                    seedingSQLDBFromZip.SeedSalesTable(excelData);
                }
            }
        }
    }
}
