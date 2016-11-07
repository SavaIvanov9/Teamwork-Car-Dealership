using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Common;
using Dealership.ExcelReportGenerator.Contracts;
using Dealership.MySQL;
using Dealership.SQLite;

namespace Dealership.ExcelReportGenerator
{
    class Test
    {
        static void Main(string[] args)
        {
            //TestSQLite();
            //TestMySQL();
            GenerateExcelReportFromMySqlAndSqLite();
        }

        private static void TestSQLite()
        {
            Console.WriteLine("test sqlite");


            var sqliteShits = new List<Item>();

            using (var db = new ItemExpensesDbEntities())
            {
                var items = db.Items.ToList();
                sqliteShits = items;
                if (db.Items.Select(x => x.Name).Contains("M4"))
                {
                    Console.WriteLine(true);
                }
                else
                {
                    Console.WriteLine(false);
                }
            }

            foreach (var shit in sqliteShits)
            {
                Console.WriteLine($"{shit.ItemId} {shit.Name} {shit.Taxes}");
            }
        }

        private static void TestMySQL()
        {
            Console.WriteLine("test mysql");

            using (var dbContext = new DaDbContext())
            {
                var items = dbContext.JsonReports.ToList();
                foreach (var r in items)
                {
                    Console.WriteLine($"{r.Id} {r.JsonContent}");
                }
            }
        }

        private static void GenerateExcelReportFromMySqlAndSqLite()
        {
            IExcelReportGenerator excelReportGenerator = new ReportGenerator();

            string reportsPath = Constants.ExtractedExcelReportsPath;
            string excelReportName = Constants.ExcelReportName;

            excelReportGenerator.GenerateExcelReport(reportsPath, excelReportName);

            try
            {



            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured! Cannot create Excel Report file.");

            }
        }
    }
}
