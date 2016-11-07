using Dealership.Data;
using Dealership.MySQL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.OpenAccess;

namespace Dealership.JsonReporter
{
    class StartUp
    {
        static void Main(string[] args)
        {
            ICollection<JsonReport> jsonReports = new HashSet<JsonReport>();

            // creating json reports.
            using (var dbContext = new DealershipDbContext())
            {
                var vehicles = dbContext.Vehicles.ToList();
                var sales = dbContext.Sales;
                string directoryPath = "../../../Json-Reports";
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                foreach (var item in vehicles)
                {
                    int reportId = item.Id;
                    var totalQuantitySold = sales.Where(x => x.VehicleId == reportId).Sum(x => x.Quantity) ?? 0;
                    var totalIncome = sales.Where(x => x.VehicleId == reportId).Sum(x => x.Price*x.Quantity) ?? 0.00m;

                    var jsonReportEntry = new JsonReportEntry()
                    {
                        ProductId = reportId,
                        ProductName = item.Model,
                        ManufacturerName = item.Brand.Name,
                        TotalQuantitySold = totalQuantitySold,
                        TotalIncome = totalIncome
                    };

                    string jsonObj = JsonConvert.SerializeObject(jsonReportEntry, Formatting.Indented);
                    var jsonReport = new JsonReport()
                    {
                        Id = reportId,
                        JsonContent = jsonObj
                    };
                    jsonReports.Add(jsonReport);

                    //File.WriteAllText($"{directoryPath}/{reportId}.json", jsonObj);
                    using(var writer =  new StreamWriter(string.Format("{0}/{1}.json",directoryPath, reportId)))
                    {
                        writer.WriteLine(jsonObj);
                    }
                }
                Console.WriteLine("JSON Reports created!");
            }

            // uploding reports to MySQL
            UpdateDatabase();
            using (var dbContext = new DaDbContext())
            {
                var dbreports = dbContext.GetAll<JsonReport>();
                foreach (var item in jsonReports)
                {
                    if (dbreports.FirstOrDefault(x=>x.Id==item.Id) == null)
                    {
                        dbContext.Add(item);
                    }
                }
                dbContext.SaveChanges();
                Console.WriteLine("Reports Updated to MySQL database!");
            }
        }

        private static void UpdateDatabase()
        {
            using (var context = new DaDbContext())
            {
                var schemaHandler = context.GetSchemaHandler();
                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            string script = null;
            if (schemaHandler.DatabaseExists())
            {
                script = schemaHandler.CreateUpdateDDLScript(null);
            }
            else
            {
                schemaHandler.CreateDatabase();
                script = schemaHandler.CreateDDLScript();
            }

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }
    }
}
