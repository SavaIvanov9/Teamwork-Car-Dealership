using Dealership.Models.Models.MongoDbSource;

namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface ITire : IEntity
    {
        int TireTypeId { get; set; }

        TireType TireType { get; set; }

        int TireBrandId { get; set; }

        TireBrand TyreBrand { get; set; }

        decimal Cost { get; set; }
    }
}
