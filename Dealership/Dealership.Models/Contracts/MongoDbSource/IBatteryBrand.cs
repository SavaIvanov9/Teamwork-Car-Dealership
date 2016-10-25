using System.Collections.Generic;

using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IBatteryBrand : IEntity
    {
        string Name { get; set; }

        ICollection<Battery> Batteries { get; }
    }
}
