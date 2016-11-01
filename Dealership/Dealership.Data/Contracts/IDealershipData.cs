using System;

using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.XmlSource;

namespace Dealership.Data.Contracts
{
    public interface IDealershipData : IDisposable
    {
        IDealershipRepository<Vehicle> Vehicles { get; }

        IDealershipRepository<VehicleType> VehicleTypes { get; }

        IDealershipRepository<Brand> Brands { get; }

        IDealershipRepository<Fuel> Fuels { get; }

        IDealershipRepository<Address> Addresses { get; }

        IDealershipRepository<Employee> Employees { get; }

        IDealershipRepository<City> Cities { get; }

        IDealershipRepository<Country> Countries { get; }

        IDealershipRepository<Position> Positions { get; }

        IDealershipRepository<Shop> Shops { get; }

        int SaveChanges();
    }
}
