using MongoDB.Driver;
using RealEstate.Domain.Abstractions;
using RealEstate.Domain.Entities;
using RealEstate.Domain.Queries;

namespace RealEstate.Infrastructure.Mongo;

public sealed class PropertyReadRepository(MongoContext ctx) : IPropertyReadRepository
{
    public async Task<(IReadOnlyList<Property> Items, long Total)> SearchAsync(
        PropertySearchCriteria criteria, CancellationToken ct = default)
    {
        var fb = Builders<Property>.Filter;
        var filter = fb.Empty;

        if (!string.IsNullOrWhiteSpace(criteria.Name))
        {
            // búsqueda insensible a mayúsculas
            filter &= fb.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(criteria.Name, "i"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.Address))
        {
            filter &= fb.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(criteria.Address, "i"));
        }

        if (criteria.MinPrice is not null)
        {
            filter &= fb.Gte(x => x.Price, criteria.MinPrice.Value);
        }

        if (criteria.MaxPrice is not null)
        {
            filter &= fb.Lte(x => x.Price, criteria.MaxPrice.Value);
        }

        var find = ctx.Properties.Find(filter);

        var total = await find.CountDocumentsAsync(ct);

        var items = await find
            .SortByDescending(p => p.Price)   // ejemplo de sort por precio
            .Skip(criteria.Skip)
            .Limit(criteria.Take)
            .ToListAsync(ct);

        return (items, total);
    }
}
