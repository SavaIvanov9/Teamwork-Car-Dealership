using Dealership.Common;
using Dealership.JsonReporter.Modules;
using Dealership.MySQL;
using Newtonsoft.Json;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik.OpenAccess;

namespace Dealership.JsonReporter
{
    public static class JsonReports
    {
        public static ICollection<JsonReport> GenerateReports(string directoryPath)
        {

            var kernel = new StandardKernel(new EntityFrameworkModule());
            var dp = kernel.Get<SQLServerDataProvider>();

            ICollection<JsonReport> jsonReports = new HashSet<JsonReport>();

            using (var uow = dp.UnitOfWork())
            {
                var vehicles = dp.Vehicles.GetAll();
                var sales = dp.Sales.GetAll();

                Utility.CreateDirectoryIfNotExists(directoryPath);

                foreach (var item in vehicles)
                {
                    int reportId = item.Id;
                    var totalQuantitySold = sales.Where(x => x.VehicleId == reportId).Sum(x => x.Quantity) ?? 0;
                    var totalIncome = sales.Where(x => x.VehicleId == reportId).Sum(x => x.Price * x.Quantity) ?? 0.00m;

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

                    using (var writer = new StreamWriter(string.Format("{0}/{1}.json", directoryPath, reportId)))
                    {
                        writer.WriteLine(jsonObj);
                    }
                }
            }

            return jsonReports;
        }

        public static void SeedReportsToMySQL(ICollection<JsonReport> jsonReports)
        {
            var kernel = new StandardKernel(new DataAccessModule());
            UpdateDatabase();

            var dp = kernel.Get<MySQLDataProvider>();
            using (var uow = dp.UnitOfWork())
            {
                var mysqlReports = dp.JsonReports.GetAll();
                foreach (var item in jsonReports)
                {
                    if (mysqlReports.FirstOrDefault(x => x.Id == item.Id) == null)
                    {
                        dp.JsonReports.Add(item);
                    }
                }
                uow.Commit();
            }
        }

        private static void UpdateDatabase()
        {
            using (var context = new DataAccessDbContext())
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
