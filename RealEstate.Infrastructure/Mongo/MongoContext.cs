using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mongo;

public sealed class MongoContext
{
    public IMongoDatabase Database { get; }
    public IMongoCollection<Property> Properties { get; }
    private static bool _isRegistered = false;

    public MongoContext(MongoSettings settings)
    {
        // Registrar serializer personalizado una sola vez
        if (!_isRegistered)
        {
            BsonClassMap.RegisterClassMap<Property>(cm =>
            {
                cm.AutoMap();
                cm.MapMember(c => c.IdOwner).SetSerializer(new FlexibleStringSerializer());
            });
            _isRegistered = true;
        }
        
        var client = new MongoClient(settings.ConnectionString);
        Database = client.GetDatabase(settings.Database);
        Properties = Database.GetCollection<Property>(settings.PropertiesCollection);
    }
}
