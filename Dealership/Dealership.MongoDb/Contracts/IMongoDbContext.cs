using MongoDB.Driver;

namespace Dealership.MongoDb.Contracts
{
    public interface IMongoDbContext
    {
        IMongoDatabase Context { get; set; }
    }
}
