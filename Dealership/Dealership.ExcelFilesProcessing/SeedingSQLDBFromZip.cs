using Dealership.Data;
using Dealership.Models;
using Dealership.Models.Models.SalesReportSource;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dealership.Reports.Models;
using Dealership.Models.Models.XmlSource;
using DealerShip.Models;

namespace Dealership.ExcelFilesProcessing
{
    public class SeedingSQLDBFromZip
    {
        private int GetShopIdByName(string name, DealershipDbContext dealershipDbContext)
        {
            var data = new DealershipData(dealershipDbContext);
            var shop = data.Shops.Search(s => s.Name.ToLower() == name.ToLower()).ToList();

            if (shop == null)
            {
                throw new ArgumentException("No such shop name in collection!");
            }

            return shop[0].Id;
        }
        private int GetVehicleIdByModel(string model, DealershipDbContext dealershipDbContext)
        {
            var data = new DealershipData(dealershipDbContext);
            var vehicle = data.Vehicles.Search(v => v.Model.ToLower().Contains(model.ToLower())).ToList();

            if (vehicle == null)
            {
                throw new ArgumentException("No such vehicle model in collection!");
            }

            return vehicle[0].Id;
        }
        private bool ValidateEmployeeId(int EmployeeId, DealershipDbContext dealershipDbContext)
        {
            var data = new DealershipData(dealershipDbContext);
            var employee = data.Employees.Search(s => s.Id == EmployeeId).FirstOrDefault();

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
            using (var dealershipDbContext = new DealershipDbContext()) //TODO PASS AS ARGUMENT
            {
                int shopId = GetShopIdByName(shopName, dealershipDbContext); //TODO REMOVE LOWERDASH WHEN THE 2 TABLES CONVERGE
                foreach (var record in excelSalesReport.Records)
                {
                    ValidateEmployeeId(record.EmployeeId, dealershipDbContext);
                    Sale s = new Sale()
                    {
                        ShopId = shopId,
                        VehicleId = GetVehicleIdByModel(record.VehicleModel, dealershipDbContext),
                        EmployeeId = record.EmployeeId,
                        Quantity = record.Quantity,
                        Price = record.UnitPrice,
                        DateOfSale = excelSalesReport.DateOfSale
                    };

                    //check if shopId and productId already exist and if they do not, cancel operation
                    //Validator.ValidateId(int id, IDbSet<T>)

                    //bool productIdIsValid = true;// ValidateId(s.ProductId, salesReportsDBContext.Products);

                    dealershipDbContext.Sales.Add(s);
                    //throw new ArgumentException("No such product or shop id in database!"); //TODO HANDLE EXCEPTION
                }


                dealershipDbContext.SaveChanges();
            }
        }

    }
}
