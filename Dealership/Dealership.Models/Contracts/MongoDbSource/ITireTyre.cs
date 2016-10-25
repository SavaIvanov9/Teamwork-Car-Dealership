using System.Collections.Generic;

using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface ITireTyre : IEntity
    {
        string Type { get; set; }

        ICollection<Tire> Tires { get; }
    }
}
