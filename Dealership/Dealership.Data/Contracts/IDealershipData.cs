using System;

using Dealership.Models.Models.MongoDbSource;

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

        int SaveChanges();
    }
}
