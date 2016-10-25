using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class BatteryBrand : IBatteryBrand
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public BatteryBrand()
        {

        }

        public BatteryBrand(string name)
        {
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
