using Dealership.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.ExcelFilesProcessing
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            SeedingSQLDBFromZip s = new SeedingSQLDBFromZip();
            s.DataGenerator();
            return;
            //SalesReportsReaderExcel excelReader = new SalesReportsReaderExcel();
            //excelReader.ReadReport(@"D:\_TELERIK\Database\TestingTeamProject\ExperimentalProject\Data\Bourgas.xls", "10-Oct-2014");

            var processor = new ZipUnpacker();
            processor.Unpack(Constants.PathToZipFile, Constants.PathToUnzip);

            var matchingDirectories = Utility.GetDirectoriesByPattern(Constants.PathToUnzippedFiles);
            ReportReader reportReader = new ReportReader();
            reportReader.ParseExcelData(matchingDirectories);
        }
    }
}
