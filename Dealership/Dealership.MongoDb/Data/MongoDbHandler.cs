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
    public class MongoDbHandler : IMongoDbHandler
    {
        private const string PathToXml = "../../../Dealership.MongoDb/XmlSource/Vehicles.xml";
        private string connectionString;

        private string databaseName;

        private IMongoDbRepository<MongoDbVehicle> vehicles;

        private IMongoDbContext database;

        public MongoDbHandler(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
            this.database = this.LoadData(this.connectionString, this.databaseName);
            this.vehicles = this.GetVehicleRepositoryFromMongo(this.database);
        }

        public bool IsDataSeeded(IDealershipDbContext data)
        {
            return data.Vehicles.Any();
        }

        public void SeedData(IDealershipDbContext data)
        {
            this.LoadVehiclesFromXml();

            this.LoadVehiclesToSql(data);
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

            var resultCollection = new List<XmlVehicle>();

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

                this.vehicles.Add(mongoDbVehicle);
            }
        }

        private void LoadVehiclesToSql(IDealershipDbContext data)
        {
            foreach (var vehicle in this.vehicles.All().ToList())
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
