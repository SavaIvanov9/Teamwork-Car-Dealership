using Dealership.MongoDb.Contracts;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dealership.MongoDb.Models
{
    public class MongoDbTire : IMongoDbTire
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string Brand { get; set; }

        public string TireType { get; set; }

        public decimal Cost { get; set; }
    }
}
