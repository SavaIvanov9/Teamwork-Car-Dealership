using System;

using Dealership.Models.Contracts.MongoDbSource;

namespace Dealership.Models.Models.MongoDbSource
{
    public class Tire : ITire
    {
        [Obsolete("Only needed for serialization and materialization", true)]
        public Tire()
        {

        }

        public Tire(int tireTypeId, int tireBrandId, decimal cost)
        {
            this.TireTypeId = tireTypeId;
            this.TireBrandId = tireBrandId;
            this.Cost = cost;
        }

        public int Id { get; set; }

        public int TireTypeId { get; set; }

        public TireType TireType { get; set; }

        public int TireBrandId { get; set; }

        public TireBrand TyreBrand { get; set; }

        public decimal Cost { get; set; }
    }
}
