using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Vehicle : IVehicle
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public Vehicle()
        {

        }

        public Vehicle(string model, int brandId, int fuelId, int vehicleTypeId, int year, decimal cost)
        {
            this.Model = model;
            this.BrandId = brandId;
            this.FuelId = fuelId;
            this.VehicleTypeId = vehicleTypeId;
            this.Year = year;
            this.Cost = cost;
        }

        public int Id { get; set; }

        public string Model { get; set; }

        public int VehicleTypeId { get; set; }

        public VehicleType VehicleType { get; set; }

        public int BrandId { get; set; }

        public Brand Brand { get; set; }

        public int FuelId { get; set; }

        public Fuel Fuel { get; set; }

        public int Year { get; set; }

        public decimal Cost { get; set; }
    }
}
