using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class VehicleType : IVehicleType
    {
        private ICollection<Vehicle> vehicles;

        public VehicleType()
        {
            this.vehicles = new HashSet<Vehicle>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public virtual ICollection<Vehicle> Vehicles
        {
            get
            {
                return this.vehicles;
            }

            set
            {
                this.vehicles = value;
            }
        }
    }
}
