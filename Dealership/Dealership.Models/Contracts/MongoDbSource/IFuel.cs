using System.Collections.Generic;

using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IFuel : IEntity
    {
        string Name { get; set; }

        ICollection<Vehicle> Vehicles { get; }
    }
}
