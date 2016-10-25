using Dealership.MongoDb.Contracts;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dealership.MongoDb.Models
{
    public class MongoDbVehicle : IMongoDbVehicle
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; set; }

        public string Type { get; set; }

        public string Model { get; set; }

        public string Brand { get; set; }

        public string Fuel { get; set; }

        public int Year { get; set; }

        public decimal Cost { get; set; }
    }
}
