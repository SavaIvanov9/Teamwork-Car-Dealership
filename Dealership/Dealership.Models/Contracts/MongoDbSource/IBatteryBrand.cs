namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IBatteryBrand : IEntity
    {
        string Name { get; set; }
    }
}
