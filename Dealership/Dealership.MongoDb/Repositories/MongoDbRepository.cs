using System.Linq;

using Dealership.MongoDb.Contracts;

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Dealership.MongoDb.Repositories
{
    public class MongoDbRepository<T> : IMongoDbRepository<T>
               where T : IMongoDbEntity
    {
        private string collectionName;
        private IMongoDbContext db;

        public MongoDbRepository(IMongoDbContext db, string collectionName)
        {
            this.db = db;
            this.collectionName = collectionName;
        }

        public void Add(T value)
        {
            var collection = this.db.Context.GetCollection<BsonDocument>(this.collectionName);
            var valueAsBson = value.ToBsonDocument();
            collection.InsertOne(valueAsBson);
        }

        public IQueryable<T> All()
        {
            var collection = this.db.Context.GetCollection<BsonDocument>(this.collectionName);
            var bsonValues = collection.Find(new BsonDocument()).ToList();
            var values = bsonValues.Select(bsonValue => BsonSerializer.Deserialize<T>(bsonValue));

            return values.AsQueryable();
        }

        public void Delete(T obj)
        {
            this.Delete(obj.Id);
        }

        public void Delete(object id)
        {
            var objectId = new ObjectId(id.ToString());
            var filter = Builders<BsonDocument>.Filter.Eq("_id", objectId);

            var collection = this.db.Context.GetCollection<BsonDocument>(this.collectionName);
            collection.DeleteOneAsync(filter);
        }
    }
}
