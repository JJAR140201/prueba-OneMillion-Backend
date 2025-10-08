using MongoDB.Driver;
using RealEstate.Domain.Abstractions;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Mongo;

public sealed class PropertyWriteRepository(MongoContext ctx) : IPropertyWriteRepository
{
    public async Task<Property> CreateAsync(Property property, CancellationToken ct = default)
    {
        await ctx.Properties.InsertOneAsync(property, cancellationToken: ct);
        return property;
    }

    public async Task<bool> UpdateAsync(Property property, CancellationToken ct = default)
    {
        var filter = Builders<Property>.Filter.Eq(x => x.Id, property.Id);
        var result = await ctx.Properties.ReplaceOneAsync(filter, property, cancellationToken: ct);
        
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<Property>.Filter.Eq(x => x.Id, id);
        var result = await ctx.Properties.DeleteOneAsync(filter, ct);
        
        return result.DeletedCount > 0;
    }

    public async Task<Property?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<Property>.Filter.Eq(x => x.Id, id);
        return await ctx.Properties.Find(filter).FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken ct = default)
    {
        var filter = Builders<Property>.Filter.Eq(x => x.Id, id);
        var count = await ctx.Properties.CountDocumentsAsync(filter, cancellationToken: ct);
        
        return count > 0;
    }

    public async Task ClearAllAsync(CancellationToken ct = default)
    {
        await ctx.Properties.DeleteManyAsync(Builders<Property>.Filter.Empty, ct);
    }
}