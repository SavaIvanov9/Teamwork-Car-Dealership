using Dealership.MongoDb.Contracts;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dealership.MongoDb.Models
{
    public class MongoDbBattery : IMongoDbBattery
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string Brand { get; set; }

        public int AmperHours { get; set; }

        public decimal Cost { get; set; }
    }
}
