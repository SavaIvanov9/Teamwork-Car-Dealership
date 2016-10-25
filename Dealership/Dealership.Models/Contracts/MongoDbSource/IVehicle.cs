using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IVehicle : IEntity
    {
        string Model { get; set; }
        int BrandId { get; set; }

        Brand Brand { get; set; }

        int VehicleTypeId { get; set; }

        VehicleType VehicleType { get; set; }

        int FuelId { get; set; }

        Fuel Fuel { get; set; }

        int Year { get; set; }

        decimal Cost { get; set; }
    }
}
