using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IBattery
    {
        int AmperHours { get; set; }

        BatteryBrand BatteryBrand { get; set; }

        int BatteryBrandId { get; set; }

        decimal Cost { get; set; }

        int Id { get; set; }
    }
}