using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RealEstate.Domain.Entities;

public sealed class Property
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = default!;
    
    // IdOwner como string normal, no como ObjectId
    public string IdOwner { get; init; } = default!;
    
    public string Name { get; init; } = default!;
    public string Address { get; init; } = default!;
    public decimal Price { get; init; }
    public string? CodeInternal { get; init; }
    public int? Year { get; init; }
    
    // Propiedad calculada para obtener la URL de imagen desde propertyImages
    [BsonIgnore]
    public string ImageUrl => ""; // Por ahora vacío, se puede implementar después
}
