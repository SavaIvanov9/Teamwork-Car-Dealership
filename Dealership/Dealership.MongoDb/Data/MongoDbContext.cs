using Dealership.MongoDb.Contracts;

using MongoDB.Driver;

namespace Dealership.MongoDb.Data
{
    public class MongoDbContext : IMongoDbContext
    {
        public MongoDbContext(string connectionString, string databaseName)
        {
            this.Context = this.GetDatabase(connectionString, databaseName);
        }

        public IMongoDatabase Context { get; set; }

        private IMongoDatabase GetDatabase(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            return client.GetDatabase(databaseName);
        }
    }
}
