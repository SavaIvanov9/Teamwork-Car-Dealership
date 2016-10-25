using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class VehicleType : IVehicleType
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public VehicleType()
        {

        }

        public VehicleType(string name)
        {
            this.Type = name;
        }

        public int Id { get; set; }

        public string Type { get; set; }
    }
}
