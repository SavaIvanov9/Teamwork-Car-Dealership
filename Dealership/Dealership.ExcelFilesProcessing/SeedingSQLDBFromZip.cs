using Dealership.ExcelFilesProcessing.Models;
using DealerShip.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.ExcelFilesProcessing
{
    public class SeedingSQLDBFromZip
    {
        public bool ValidateId(int id, IDbSet<Product> products) //TODO abstract
        {

            var s = from p in products
                    where (p.Id == id)
                    select (p.Id);

            if (s.Count() == 0)
            {
                return false;
            }

            return true;
        }

        private int GetShopIdByName(string name, IDbSet<Shop> shops)
        {
            var shop = shops
                .Where(s => s.Name.ToLower() == name.ToLower())
                .FirstOrDefault();

            if (shop == null)
            {
                throw new ArgumentException("No such shop name in collection!");
            }

            return shop.Id;
        }
        public void DataGenerator()
        {
            using (var salesReportsDBContext = new SalesReportsDBContext())
            {
                for (int i = 1; i < 10; i++)
                {
                    Product p = new Product()
                    {
                        Name = "name" + i
                    };
                    salesReportsDBContext.Products.Add(p);
                }
                for (int i = 1; i < 10; i++)
                {
                    Shop sh = new Shop()
                    {
                        Name = "nameShop" + i,
                        AddressId = i
                    };
                    salesReportsDBContext.Shops.Add(sh);
                }
                salesReportsDBContext.SaveChanges();

                for (int i = 1; i < 10; i++)
                {
                    Sale s = new Sale()
                    {
                        ShopId = i,
                        ProductId = i,
                        Quantity = 2,
                        Price = i,
                        DateOfSale = DateTime.Now
                    };
                    salesReportsDBContext.Sales.Add(s);
                }
                salesReportsDBContext.SaveChanges();
            }
        }
        public void SeedSalesTable(ExcelSalesReport excelSalesReport) //SalesReportEntry
        {

            string shopName = excelSalesReport.DistributorName; //.Split('"')[1];   //sample: Supermarket “Bourgas – Plaza”

            //TODO TRY CATCH
            using (var salesReportsDBContext = new SalesReportsDBContext()) //TODO PASS AS ARGUMENT
            {
                int shopId = 1; // GetShopIdByName(shopName, salesReportsDBContext.Shops_); //TODO REMOVE LOWERDASH WHEN THE 2 TABLES CONVERGE

                foreach (var record in excelSalesReport.Records)
                {
                    Sale s = new Sale()
                    {
                        ShopId = shopId,
                        ProductId = record.ProductId,
                        Quantity = (float)record.Quantity,
                        Price = record.UnitPrice,
                        DateOfSale = excelSalesReport.DateOfSale
                    };

                    //check if shopId and productId already exist and if they do not, cancel operation
                    //Validator.ValidateId(int id, IDbSet<T>)

                    bool productIdIsValid = true;// ValidateId(s.ProductId, salesReportsDBContext.Products);

                    if (productIdIsValid)
                    {
                        // salesReportsDBContext.Sales.Add(s);
                    }
                    else
                    {
                        throw new ArgumentException("No such product or shop id in database!"); //TODO HANDLE EXCEPTION
                    }
                }


                //salesReportsDBContext.SaveChanges();
            }
        }

    }
}
