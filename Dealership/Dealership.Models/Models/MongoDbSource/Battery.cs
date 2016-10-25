using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Battery : IBattery
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public Battery()
        {

        }

        public Battery(int batteryBrandId, int ah, decimal cost)
        {
            this.BatteryBrandId = batteryBrandId;
            this.AmperHours = ah;
            this.Cost = cost;
        }

        public int Id { get; set; }

        public int BatteryBrandId { get; set; }

        public BatteryBrand BatteryBrand { get; set; }

        public int AmperHours { get; set; }

        public decimal Cost { get; set; }
    }
}
