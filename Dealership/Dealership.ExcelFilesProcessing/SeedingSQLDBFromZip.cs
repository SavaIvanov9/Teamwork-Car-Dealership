using Dealership.Models.Models.SalesReportSource;
using System;
using System.Linq;
using DealerShip.Reports.Models;
using Dealership.Data.Contracts;
using Dealership.Models.Models.XmlSource;
using Dealership.Models.Models.MongoDbSource;

namespace Dealership.ExcelFilesProcessing
{
    public class SeedingSQLDBFromZip
    {
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
        public void SeedSalesTable(ExcelSalesReport excelSalesReport) //SalesReportEntry
        {

            string shopName = excelSalesReport.DistributorName; //.Split('"')[1];   //sample: Supermarket “Bourgas – Plaza”

            //TODO TRY CATCH
            int shopId = GetShopIdByName(shopName); //TODO REMOVE LOWERDASH WHEN THE 2 TABLES CONVERGE
            foreach (var record in excelSalesReport.Records)
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

                //check if shopId and productId already exist and if they do not, cancel operation
                //Validator.ValidateId(int id, IDbSet<T>)

                //bool productIdIsValid = true;// ValidateId(s.ProductId, salesReportsDBContext.Products);

                this.sales.Add(s);
                //throw new ArgumentException("No such product or shop id in database!"); //TODO HANDLE EXCEPTION
            }


            this.data.SaveChanges();
        }
    }
}
