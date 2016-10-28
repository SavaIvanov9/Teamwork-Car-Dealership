using System.Collections.Generic;
using System.Linq;

using Dealership.Data.Contracts;
using Dealership.Models.Models.MongoDbSource;
using Dealership.MongoDb.Contracts;
using Dealership.MongoDb.Data;
using Dealership.MongoDb.Models;
using Dealership.MongoDb.Repositories;

namespace Dealership.MongoDb
{
    public class MongoDbHandler : IMongoDbHandler
    {
        private string connectionString;

        private string databaseName;

        private IEnumerable<IMongoDbVehicle> vehicles;
        private IEnumerable<IMongoDbTire> tires;
        private IEnumerable<IMongoDbBattery> batteries;

        public MongoDbHandler(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
        }

        public bool IsDataSeeded(IDealershipDbContext data)
        {
            return data.Vehicles.Any();
        }

        public void SeedData(IDealershipDbContext data)
        {
            this.LoadRepositories();

            this.LoadBrands(data);
            this.LoadFuels(data);
            this.LoadTireTypes(data);
            this.LoadTireBrands(data);
            this.LoadBatteryBrands(data);
            this.LoadVehicleTypes(data);
            this.LoadVehicles(data);
            this.LoadTires(data);
            this.LoadBatteries(data);
        }

        private void LoadRepositories()
        {
            var database = this.LoadData();
            this.vehicles = new MongoDbRepository<MongoDbVehicle>(database, "Vehicles").All().ToList();
            this.tires = new MongoDbRepository<MongoDbTire>(database, "Tires").All().ToList();
            this.batteries = new MongoDbRepository<MongoDbBattery>(database, "Batteries").All().ToList();
        }

        private IMongoDbContext LoadData()
        {
            return new MongoDbContext(this.connectionString, this.databaseName);
        }

        private void LoadBrands(IDealershipDbContext data)
        {
            foreach (var vehicle in this.vehicles)
            {
                var brandName = vehicle.Brand;

                if (data.Brands.Local.All(b => b.Name != brandName))
                {
                    var brand = new Brand(brandName);
                    data.Brands.Add(brand);
                }
            }

            data.SaveChanges();
        }

        private void LoadFuels(IDealershipDbContext data)
        {
            foreach (var vehicle in this.vehicles)
            {
                var fuelName = vehicle.Fuel;

                if (data.Fuels.Local.All(f => f.Name != fuelName))
                {
                    var fuel = new Fuel(fuelName);
                    data.Fuels.Add(fuel);
                }
            }

            data.SaveChanges();
        }

        private void LoadVehicleTypes(IDealershipDbContext data)
        {
            foreach (var vehicle in this.vehicles)
            {
                var type = vehicle.Type;

                if (data.VehiclesTypes.Local.All(f => f.Type != type))
                {
                    var vehicleType = new VehicleType(vehicle.Type);
                    data.VehiclesTypes.Add(vehicleType);
                }
            }

            data.SaveChanges();
        }

        private void LoadTireTypes(IDealershipDbContext data)
        {
            foreach (var tire in this.tires)
            {
                if (data.TireTypes.Local.All(t => t.Type != tire.TireType))
                {
                    data.TireTypes.Add(new TireType(tire.TireType));
                }
            }

            data.SaveChanges();
        }

        private void LoadTireBrands(IDealershipDbContext data)
        {
            foreach (var tire in this.tires)
            {
                if (data.TyreBrands.Local.All(t => t.Name != tire.Brand))
                {
                    data.TyreBrands.Add(new TireBrand(tire.Brand));
                }
            }

            data.SaveChanges();
        }

        private void LoadBatteryBrands(IDealershipDbContext data)
        {
            foreach (var batt in this.batteries)
            {
                if (data.BatteryBrands.Local.All(b => b.Name != batt.Brand))
                {
                    data.BatteryBrands.Add(new BatteryBrand(batt.Brand));
                }
            }

            data.SaveChanges();
        }

        private void LoadVehicles(IDealershipDbContext data)
        {
            foreach (var vehicle in this.vehicles)
            {
                var model = vehicle.Model;
                var brandId = data.Brands.FirstOrDefault(b => b.Name == vehicle.Brand).Id;
                var fuelId = data.Fuels.FirstOrDefault(f => f.Name == vehicle.Fuel).Id;
                var vehicleTypeId = data.VehiclesTypes.FirstOrDefault(v => v.Type == vehicle.Type).Id;
                var year = vehicle.Year;
                var cost = vehicle.Cost;

                data.Vehicles.Add(new Vehicle(model, brandId, fuelId, vehicleTypeId, year, cost));
            }

            data.SaveChanges();
        }

        private void LoadTires(IDealershipDbContext data)
        {
            foreach (var tire in this.tires)
            {
                var tireBrandId = data.TyreBrands.FirstOrDefault(t => t.Name == tire.Brand).Id;
                var tireTypeId = data.TireTypes.FirstOrDefault(t => t.Type == tire.TireType).Id;
                var cost = tire.Cost;

                data.Tires.Add(new Tire(tireBrandId, tireTypeId, cost));
            }

            data.SaveChanges();
        }

        private void LoadBatteries(IDealershipDbContext data)
        {
            foreach (var batt in this.batteries)
            {
                var battBrandId = data.BatteryBrands.FirstOrDefault(b => b.Name == batt.Brand).Id;
                var ah = batt.AmperHours;
                var cost = batt.Cost;

                data.Batteries.Add(new Battery(battBrandId, ah, cost));
            }

            data.SaveChanges();
        }

    }
}
