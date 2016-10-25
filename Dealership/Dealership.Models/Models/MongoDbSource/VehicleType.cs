using System;
using System.Collections.Generic;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class VehicleType : IVehicleType
    {
        private ICollection<Vehicle> vehicles;

        [Obsolete("Only needed for serialization and materialization", true)]
        public VehicleType()
        {
            this.vehicles = new HashSet<Vehicle>();
        }

        public VehicleType(string name)
        {
            this.Type = name;
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
