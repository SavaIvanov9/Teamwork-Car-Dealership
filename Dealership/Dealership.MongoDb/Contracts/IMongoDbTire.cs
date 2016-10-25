namespace Dealership.MongoDb.Contracts
{
    public interface IMongoDbTire : IMongoDbEntity
    {
        string Brand { get; set; }

        string TireType { get; set; }

        decimal Cost { get; set; }
    }
}
