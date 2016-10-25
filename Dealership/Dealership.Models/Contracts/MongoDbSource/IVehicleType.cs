namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface IVehicleType : IEntity
    {
        string Type { get; set; }
    }
}
