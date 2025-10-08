using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace RealEstate.Infrastructure.Mongo;

/// <summary>
/// Serializer que puede deserializar tanto ObjectId como String para compatibilidad temporal
/// </summary>
public class FlexibleStringSerializer : SerializerBase<string>
{
    public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        var bsonType = context.Reader.GetCurrentBsonType();
        
        return bsonType switch
        {
            BsonType.String => context.Reader.ReadString(),
            BsonType.ObjectId => context.Reader.ReadObjectId().ToString(),
            BsonType.Null => null!,
            _ => throw new FormatException($"Cannot deserialize BsonType {bsonType} to string")
        };
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
    {
        if (value == null)
        {
            context.Writer.WriteNull();
        }
        else
        {
            context.Writer.WriteString(value);
        }
    }
}