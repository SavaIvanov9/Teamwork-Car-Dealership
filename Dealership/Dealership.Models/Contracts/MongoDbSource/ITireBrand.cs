using System.Collections.Generic;

using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    interface ITireBrand : IEntity
    {
        string Name { get; set; }

        ICollection<Tire> Tires { get; }
    }
}
