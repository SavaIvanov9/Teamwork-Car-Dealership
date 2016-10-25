namespace Dealership.Models.Contracts.MongoDbSource
{
    public interface ITireTyre : IEntity
    {
        string Type { get; set; }
    }
}
