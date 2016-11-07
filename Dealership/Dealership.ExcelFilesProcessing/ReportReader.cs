using Dealership.Common;
using Dealership.Data;
using Dealership.Data.Contracts;
using Dealership.Reports.Models;
using System.Collections.Generic;
using System.IO;

namespace Dealership.ExcelFilesProcessing
{
    public class ReportReader
    {
        private readonly SeedingSQLDBFromZip seedingSQLDBFromZip;
        private readonly IDealershipData data;

        public ReportReader(SeedingSQLDBFromZip seedingSQLDBFromZip, IDealershipData data)
        {
            this.seedingSQLDBFromZip = seedingSQLDBFromZip;
            this.data = data;
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
