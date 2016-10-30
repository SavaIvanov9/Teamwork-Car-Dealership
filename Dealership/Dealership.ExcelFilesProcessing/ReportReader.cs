using Dealership.Common;
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
            //var salesReportFactory = new SalesReportGeneratorFromExcel(this.robotsFactoryData.Stores);
            SeedingSQLDBFromZip seedingSQLDBFromZip = new SeedingSQLDBFromZip();
            var excelSaleReportReader = new SalesReportsReaderExcel(Constants.ExcelSalesReportsConnectionStringPattern);

            foreach (var dir in matchingDirectories)
            {
                foreach (var excelFile in dir.GetFiles(Constants.AllowedExcelFileExtensionPattern))
                {
                    var excelData = excelSaleReportReader.ReadReport(excelFile.FullName, dir.Name);
                    seedingSQLDBFromZip.SeedSalesTable(excelData);
                    //var salesReport = salesReportFactory.CreateSalesReport(excelData, dir.Name);
                    //this.robotsFactoryData.SalesReports.Add(salesReport);
                }
            }

            //this.robotsFactoryData.SaveChanges();
        }
    }
}
