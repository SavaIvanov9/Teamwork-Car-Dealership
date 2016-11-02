using Dealership.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.JsonReporter
{
    class StartUp
    {
        static void Main(string[] args)
        {
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
                    var totalQuantitySold = sales.Where(x => x.VehicleId == item.Id).Sum(x => x.Quantity) ?? 0;
                    var totalIncome = sales.Where(x => x.VehicleId == item.Id).Sum(x => x.Price*x.Quantity) ?? 0.00m;

                    var jsonReportEntry = new JsonReportEntry()
                    {
                        ProductId = item.Id,
                        ProductName = item.Model,
                        ManufacturerName = item.Brand.Name,
                        TotalQuantitySold = totalQuantitySold,
                        TotalIncome = totalIncome
                    };

                    string jsonObj = JsonConvert.SerializeObject(jsonReportEntry, Formatting.Indented);

                    File.WriteAllText($"{directoryPath}/{item.Id}.json", jsonObj);
                }
            }
        }
    }
}
