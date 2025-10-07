namespace RealEstate.Infrastructure.Mongo;

public sealed class MongoSettings
{
    public string ConnectionString { get; init; } = default!;
    public string Database { get; init; } = default!;
    public string PropertiesCollection { get; init; } = "properties";
}
