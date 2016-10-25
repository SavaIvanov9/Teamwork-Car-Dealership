using System.Collections.Generic;

using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IBrand
    {
        int Id { get; set; }

        string Name { get; set; }

        ICollection<Vehicle> Vehicles { get; }
    }
}