using System.Linq;

namespace Dealership.MongoDb.Contracts
{
    public interface IMongoDbRepository<T>
        where T : IMongoDbEntity
    {
        void Add(T value);

        IQueryable<T> All();

        void Delete(object id);

        void Delete(T obj);
    }
}
