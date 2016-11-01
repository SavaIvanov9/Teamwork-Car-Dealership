using System.Data.Entity;

using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.XmlSource;

namespace Dealership.Data.Contracts
{
    public interface IDealershipDbContext : IDbContext
    {
        IDbSet<Vehicle> Vehicles { get; set; }

        IDbSet<Fuel> Fuels { get; set; }

        IDbSet<Brand> Brands { get; set; }

        IDbSet<VehicleType> VehiclesTypes { get; set; }

        IDbSet<Address> Addresses { get; set; }

        IDbSet<City> Cities { get; set; }

        IDbSet<Country> Countries { get; set; }

        IDbSet<Employee> Employees { get; set; }

        IDbSet<Position> Positions { get; set; }

        IDbSet<Shop> Shops { get; set; }
    }
}
