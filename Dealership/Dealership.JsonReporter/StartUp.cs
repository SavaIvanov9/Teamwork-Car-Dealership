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
                var employees = dbContext.Vehicles.ToList();
                Directory.CreateDirectory("../../../Json-Reports");

                foreach (var item in employees)
                {
                    string jsonObj = JsonConvert.SerializeObject(item, Formatting.Indented);

                    Console.WriteLine(item.Brand.Name);

                    //Console.WriteLine(item.FirstName + " " + item.LastName);
                    //File.Create($"../../../Json-Reports/{item.Id}.json");
                    //File.WriteAllText($"../../../Json-Reports/{item.Id}.json", jsonObj);
                }
            }
        }
    }
}
