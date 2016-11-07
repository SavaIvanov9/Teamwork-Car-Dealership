using System;
using System.Linq;
using Dealership.Data.Contracts;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.SalesReportSource;
using Dealership.Models.Models.XmlSource;
using Dealership.Reports.Models;

namespace Dealership.ExcelFilesProcessing
{
    public class SeedingSQLDBFromZip
    {
        private const int maxRecordsPerLoad = 100;
        private readonly IDealershipRepository<Employee> employees;
        private readonly IDealershipRepository<Shop> shops;
        private readonly IDealershipRepository<Vehicle> vehicles;
        private readonly IDealershipRepository<Sale> sales;
        private readonly IDealershipData data;

        public SeedingSQLDBFromZip(
            IDealershipData data,
            IDealershipRepository<Employee> employees,
            IDealershipRepository<Shop> shops,
            IDealershipRepository<Sale> sales,
            IDealershipRepository<Vehicle> vehicles)
        {
            this.data = data;
            this.employees = employees;
            this.shops = shops;
            this.vehicles = vehicles;
            this.sales = sales;
        }

        private int GetShopIdByName(string name)
        {
            var shop = this.shops.Search(s => s.Name.ToLower() == name.ToLower()).ToList();

            if (shop == null)
            {
                throw new ArgumentException("No such shop name in collection!");
            }

            return shop[0].Id;
        }
        private int GetVehicleIdByModel(string model)
        {
            var vehicle = this.vehicles.Search(v => v.Model.ToLower().Contains(model.ToLower())).ToList();

            if (vehicle == null)
            {
                throw new ArgumentException("No such vehicle model in collection!");
            }

            return vehicle[0].Id;
        }
        private bool ValidateEmployeeId(int EmployeeId)
        {
            var employee = this.employees.Search(s => s.Id == EmployeeId).FirstOrDefault();

            if (employee == null)
            {
                throw new ArgumentException("No such EmployeeId name in collection!");
            }

            return true;
        }
        public void SeedSalesTable(ExcelSalesReport excelSalesReport)
        {

            string shopName = excelSalesReport.DistributorName;
            int recordsCounter = 0;

            int shopId = GetShopIdByName(shopName);

            foreach (var record in excelSalesReport.Records)
            {
                try
                {
                    ValidateEmployeeId(record.EmployeeId);

                    Sale s = new Sale()
                    {
                        ShopId = shopId,
                        VehicleId = GetVehicleIdByModel(record.VehicleModel),
                        EmployeeId = record.EmployeeId,
                        Quantity = record.Quantity,
                        Price = record.UnitPrice,
                        DateOfSale = excelSalesReport.DateOfSale
                    };

                    this.sales.Add(s);

                    recordsCounter++;

                    if (recordsCounter >= maxRecordsPerLoad)
                    {
                        this.data.SaveChanges();
                        recordsCounter = 0;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("No such product or shop id in database!/n" + ex.Message);
                }
            }

            this.data.SaveChanges();
        }
    }
}
