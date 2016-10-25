using System.Collections.Generic;

using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IVehicleType : IEntity
    {
        string Type { get; set; }

        ICollection<Vehicle> Vehicles { get; }
    }
}
