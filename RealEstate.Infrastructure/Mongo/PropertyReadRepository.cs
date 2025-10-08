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
            filter &= fb.Regex(x => x.Name, new MongoDB.Bson.BsonRegularExpression(criteria.Name, "i"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.Address))
        {
            filter &= fb.Regex(x => x.Address, new MongoDB.Bson.BsonRegularExpression(criteria.Address, "i"));
        }

        if (!string.IsNullOrWhiteSpace(criteria.IdOwner))
        {
            filter &= fb.Eq(x => x.IdOwner, criteria.IdOwner);
        }

        if (!string.IsNullOrWhiteSpace(criteria.CodeInternal))
        {
            filter &= fb.Eq(x => x.CodeInternal, criteria.CodeInternal);
        }

        if (criteria.MinPrice is not null)
        {
            filter &= fb.Gte(x => x.Price, criteria.MinPrice.Value);
        }

        if (criteria.MaxPrice is not null)
        {
            filter &= fb.Lte(x => x.Price, criteria.MaxPrice.Value);
        }

        if (criteria.Year is not null)
        {
            filter &= fb.Eq(x => x.Year, criteria.Year.Value);
        }

        if (criteria.MinYear is not null)
        {
            filter &= fb.Gte(x => x.Year, criteria.MinYear.Value);
        }

        if (criteria.MaxYear is not null)
        {
            filter &= fb.Lte(x => x.Year, criteria.MaxYear.Value);
        }

        var find = ctx.Properties.Find(filter);
        var total = await find.CountDocumentsAsync(ct);

        // Configurar ordenamiento
        var sortDefinition = GetSortDefinition(criteria.SortBy, criteria.SortOrder);
        var items = await find
            .Sort(sortDefinition)
            .Skip(criteria.Skip)
            .Limit(criteria.Take)
            .ToListAsync(ct);

        return (items, total);
    }

    private static SortDefinition<Property> GetSortDefinition(string? sortBy, string? sortOrder)
    {
        var isDescending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);

        return sortBy?.ToLowerInvariant() switch
        {
            "name" => isDescending ? Builders<Property>.Sort.Descending(x => x.Name) 
                                  : Builders<Property>.Sort.Ascending(x => x.Name),
            "address" => isDescending ? Builders<Property>.Sort.Descending(x => x.Address) 
                                     : Builders<Property>.Sort.Ascending(x => x.Address),
            "year" => isDescending ? Builders<Property>.Sort.Descending(x => x.Year) 
                                  : Builders<Property>.Sort.Ascending(x => x.Year),
            "price" or _ => isDescending ? Builders<Property>.Sort.Descending(x => x.Price) 
                                        : Builders<Property>.Sort.Ascending(x => x.Price)
        };
    }
}
