using System;

using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.XmlSource;

namespace Dealership.Data.Contracts
{
    public interface IDealershipData : IDisposable
    {
        IDealershipRepository<Vehicle> Vehicles { get; }

        IDealershipRepository<Tire> Tires { get; }

        IDealershipRepository<Battery> Batteries { get; }

        IDealershipRepository<TireType> TireTypes { get; }

        IDealershipRepository<TireBrand> TireBrands { get; }

        IDealershipRepository<VehicleType> VehicleTypes { get; }

        IDealershipRepository<BatteryBrand> BatteryBrands { get; }

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
