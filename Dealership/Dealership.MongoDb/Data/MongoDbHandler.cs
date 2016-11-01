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
            this.LoadVehicleTypes(data);
            this.LoadVehicles(data);
        }

        private void LoadRepositories()
        {
            var database = this.LoadData();
            this.vehicles = new MongoDbRepository<MongoDbVehicle>(database, "Vehicles").All().ToList();
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
    }
}
