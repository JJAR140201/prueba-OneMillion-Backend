using MongoDB.Driver;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mongo;

public sealed class MongoContext
{
    public IMongoDatabase Database { get; }
    public IMongoCollection<Property> Properties { get; }

    public MongoContext(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.Database);
        Properties = Database.GetCollection<Property>(settings.PropertiesCollection);
    }
}
