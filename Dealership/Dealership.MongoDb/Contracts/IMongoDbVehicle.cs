namespace Dealership.MongoDb.Contracts
{
    public interface IMongoDbVehicle : IMongoDbEntity
    {
        string Type { get; set; }

        string Model { get; set; }

        string Brand { get; set; }

        string Fuel { get; set; }

        int Year { get; set; }

        decimal Cost { get; set; }
    }
}
