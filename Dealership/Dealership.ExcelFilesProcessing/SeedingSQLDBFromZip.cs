using System;
using System.Linq;
using Dealership.Data.Contracts;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.SalesReportSource;
using Dealership.Models.Models.XmlSource;
using Dealership.Reports.Models;
using Dealership.Data;

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

        public void SeedSalesTable_alt(ExcelSalesReport excelSalesReport)
        {
            var employeeIdList = this.employees.All().Select(e => e.Id).ToList();
            var VehicleIdAndModel = this.vehicles.All().Select(v => new { v.Id, v.Model }).ToList();
            var ShopIdAndName = this.shops.All().Select(sh => new { sh.Id, sh.Name }).ToList();

            var shop = ShopIdAndName.Where(s => s.Name.ToLower() == excelSalesReport.DistributorName.ToLower()).FirstOrDefault();

            if (shop != null)
            {
                string formattedDateTime = String.Format("{0:yyyy/M/d HH:mm:ss}", excelSalesReport.DateOfSale);

                using (var dealershipDbContext = new DealershipDbContext())
                {
                    var result = dealershipDbContext.Database.ExecuteSqlCommand($"DELETE FROM [Sales] WHERE ShopId={shop.Id} AND DateOfSale='{formattedDateTime}'");
                }
                int counter = 0;

                foreach (var record in excelSalesReport.Records)
                {
                    var vehicle = VehicleIdAndModel.Where(v => v.Model.ToLower().Contains(record.VehicleModel.ToLower())).FirstOrDefault();
                    if (employeeIdList.Exists(i => (i == record.EmployeeId))
                        && vehicle != null)
                    {
                        Sale s = new Sale()
                        {
                            ShopId = shop.Id,
                            VehicleId = vehicle.Id,
                            EmployeeId = record.EmployeeId,
                            Quantity = record.Quantity,
                            Price = record.UnitPrice,
                            DateOfSale = excelSalesReport.DateOfSale
                        };

                        this.sales.Add(s);
                        counter++;

                        //Console.Write(".");

                        if (counter > maxRecordsPerLoad)
                        {
                            this.data.SaveChanges();
                            counter = 0;
                        }
                    }
                    else //null
                    {
                        Console.Write($"No such EmployeeId: {record.EmployeeId} or/and VehicleModel: {record.VehicleModel}");
                    }
                }


                this.data.SaveChanges();
            }
            else //null
            {
                Console.Write($"No such DistributorName{excelSalesReport.DistributorName}");
            }

        }

    }
}