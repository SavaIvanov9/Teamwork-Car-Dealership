using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Vehicle : IVehicle
    {
        public Vehicle()
        {

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
