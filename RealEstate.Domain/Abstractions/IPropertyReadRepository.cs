using RealEstate.Domain.Entities;
using RealEstate.Domain.Queries;

namespace RealEstate.Domain.Abstractions;

public interface IPropertyReadRepository
{
    Task<(IReadOnlyList<Property> Items, long Total)> SearchAsync(
        PropertySearchCriteria criteria,
        CancellationToken ct = default);
}
