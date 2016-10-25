namespace Dealership.MongoDb.Contracts
{
    public interface IMongoDbBattery : IMongoDbEntity
    {
        string Brand { get; set; }

        int AmperHours { get; set; }

        decimal Cost { get; set; }
    }
}
