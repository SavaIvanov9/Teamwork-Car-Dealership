using Dealership.Data.Contracts;

namespace Dealership.MongoDb
{
    public interface IMongoDbHandler
    {
        void SeedData(IDealershipDbContext data);

        bool IsDataSeeded(IDealershipDbContext data);
    }
}
