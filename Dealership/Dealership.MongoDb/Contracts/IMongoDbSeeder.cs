using Dealership.Data.Contracts;

namespace Dealership.MongoDb
{
    public interface IMongoDbSeeder
    {
        void SeedData();

        bool IsDataSeeded();
    }
}
