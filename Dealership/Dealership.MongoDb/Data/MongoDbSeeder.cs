using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

using Dealership.Data.Contracts;
using Dealership.Models.Models.MongoDbSource;
using Dealership.MongoDb.Contracts;
using Dealership.MongoDb.Data;
using Dealership.MongoDb.Models;
using Dealership.MongoDb.Repositories;

namespace Dealership.MongoDb
{
    public class MongoDbSeeder : IMongoDbSeeder
    {
        private const string PathToXml = "../../../Dealership.MongoDb/XmlSource/Vehicles.xml";

        private readonly IMongoDbContext mongoDatabase;

        private IDealershipData data;
        private IMongoDbRepository<MongoDbVehicle> mongoVehicles;
        private IDealershipRepository<Vehicle> vehicles;
        private IDealershipRepository<Brand> brands;
        private IDealershipRepository<Fuel> fuels;
        private IDealershipRepository<VehicleType> vehicleTypes;

        public MongoDbSeeder(
            string connectionString, 
            string databaseName,
            IDealershipData data,
            IDealershipRepository<Vehicle> vehicles, 
            IDealershipRepository<Brand> brands, 
            IDealershipRepository<Fuel> fuels, 
            IDealershipRepository<VehicleType> vehicleTypes
            )
        {
            this.mongoDatabase = this.LoadData(connectionString, databaseName);
            this.mongoVehicles = this.GetVehicleRepositoryFromMongo(this.mongoDatabase);
            this.data = data;
            this.vehicles = vehicles;
            this.brands = brands;
            this.fuels = fuels;
            this.vehicleTypes = vehicleTypes;
        }

        public bool IsDataSeeded()
        {
            return this.vehicles.Any();
        }

        public void SeedData()
        {
            this.LoadVehiclesFromXml();

            this.LoadVehiclesToSql();
        }

        private MongoDbRepository<MongoDbVehicle> GetVehicleRepositoryFromMongo(IMongoDbContext db)
        {
            return new MongoDbRepository<MongoDbVehicle>(db, "Vehicles");
        }

        private IMongoDbContext LoadData(string connString, string dbName)
        {
            return new MongoDbContext(connString, dbName);
        }

        private IEnumerable<XmlVehicle> GetVehiclesFromXml()
        {
            var path = PathToXml;
            var serializer = new XmlSerializer(typeof(List<XmlVehicle>), new XmlRootAttribute("Vehicles"));
            var reader = new FileStream(path, FileMode.Open);

            List<XmlVehicle> resultCollection;

            using (reader)
            {
                resultCollection = (List<XmlVehicle>)serializer.Deserialize(reader);
            }

            return resultCollection;
        }

        private void LoadVehiclesFromXml()
        {
            var xmlVehicles = this.GetVehiclesFromXml();
            foreach (var xmlVehicle in xmlVehicles)
            {
                var mongoDbVehicle = new MongoDbVehicle()
                                         {
                                            Brand = xmlVehicle.Brand,
                                            Model = xmlVehicle.Model,
                                            Type = xmlVehicle.Type,
                                            Year = xmlVehicle.Year,
                                            Cost = xmlVehicle.Cost,
                                            Fuel = xmlVehicle.Fuel
                                         };

                this.mongoVehicles.Add(mongoDbVehicle);
            }
        }

        private void LoadVehiclesToSql()
        {
            foreach (var vehicle in this.mongoVehicles.All())
            {
                var brand = this.brands.FirstOrDefault(b => b.Name == vehicle.Brand);
                if (brand == null)
                {
                    brand = new Brand() { Name = vehicle.Brand };
                }

                var fuel = this.fuels.FirstOrDefault(f => f.Name == vehicle.Fuel);
                if (fuel == null)
                {
                    fuel = new Fuel() { Name = vehicle.Fuel };
                }

                var vehicleType = this.vehicleTypes.FirstOrDefault(v => v.Type == vehicle.Type);
                if (vehicleType == null)
                {
                    vehicleType = new VehicleType() { Type = vehicle.Type };
                }

                var model = vehicle.Model;
                var year = vehicle.Year;
                var cost = vehicle.Cost;

                var dataVehicle = new Vehicle() { Model = model, Brand = brand, VehicleType = vehicleType, Fuel =  fuel, Cost = cost, Year = year };

                this.vehicles.Add(dataVehicle);

                this.data.SaveChanges();
            }
        }

        
    }
}
