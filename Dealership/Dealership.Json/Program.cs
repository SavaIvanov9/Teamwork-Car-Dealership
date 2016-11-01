using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.Json
{
    class Program
    {
        private readonly ReportQueries reportQueries;
        static void Main(string[] args)
        {
            GenerateJsonReportsAndSaveThemToDisk();
        }

        public void GenerateJsonReportsAndSaveThemToDisk(string folderPath)
        {
            //try
            //{
                var jsonProductsReportsEntries = this.reportQueries.GetJsonProductsReportsFromDatabase();
                var productReportsToJsonFactory = new JsonReportsFactoryFromMsSqlDatabase(jsonProductsReportsEntries);
                productReportsToJsonFactory.SaveJsonProductsReportsToDisk(folderPath);
                //this.logger.ShowMessage("Json reports were successfully saved to disk...");
                Console.WriteLine("Json reports were successfully saved to disk...");
            //}
            //catch (Exception ex)
            //{
            //    this.logger.ShowMessage("Error! Cannot export JSON reports or save them to disk...");
            //}
        }

        //public void GenerateJsonReportsAndExportThemToMySql()
        //{
        //    try
        //    {
        //        var jsonProductsReportsEntries = this.reportQueries.GetJsonProductsReportsFromDatabase();
        //        var productReportsToJsonFactory = new JsonReportsFactoryFromMsSqlDatabase(jsonProductsReportsEntries);
        //        productReportsToJsonFactory.ExportJsonProductsReportsToMySqlDatabase();
        //        this.logger.ShowMessage("Json reports were successfully exported to MySql database...");
        //    }
        //    catch (Exception)
        //    {
        //        this.logger.ShowMessage("Error! Cannot export JSON reports to MySQL Database...");
        //    }
        //}
    }
}
