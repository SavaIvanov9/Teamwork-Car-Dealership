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
            this.GetVehicleRepositoryFromMongo();

            this.LoadVehicles(data);
        }

        private void GetVehicleRepositoryFromMongo()
        {
            var database = this.LoadData();
            this.vehicles = new MongoDbRepository<MongoDbVehicle>(database, "Vehicles").All().ToList();
        }

        private IMongoDbContext LoadData()
        {
            return new MongoDbContext(this.connectionString, this.databaseName);
        }

        private void LoadVehicles(IDealershipDbContext data)
        {
            foreach (var vehicle in this.vehicles)
            {
                var brand = data.Brands.FirstOrDefault(b => b.Name == vehicle.Brand);
                if (brand == null)
                {
                    brand = new Brand() { Name = vehicle.Brand };
                }

                var fuel = data.Fuels.FirstOrDefault(f => f.Name == vehicle.Fuel);
                if (fuel == null)
                {
                    fuel = new Fuel() { Name = vehicle.Fuel };
                }

                var vehicleType = data.VehiclesTypes.FirstOrDefault(v => v.Type == vehicle.Type);
                if (vehicleType == null)
                {
                    vehicleType = new VehicleType() { Type = vehicle.Type };
                }

                var model = vehicle.Model;
                var year = vehicle.Year;
                var cost = vehicle.Cost;

                var dataVehicle = new Vehicle() { Model = model, Brand = brand, VehicleType = vehicleType, Fuel =  fuel, Cost = cost, Year = year };

                data.Vehicles.Add(dataVehicle);

                data.SaveChanges();
            }
        }
    }
}
