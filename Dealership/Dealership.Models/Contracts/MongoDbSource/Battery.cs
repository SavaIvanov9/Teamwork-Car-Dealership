using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IBattery : IEntity
    {
        int BatteryBrandId { get; set; }

        BatteryBrand BatteryBrand { get; set; }

        int AmperHours { get; set; }

        decimal Cost { get; set; }
    }
}
