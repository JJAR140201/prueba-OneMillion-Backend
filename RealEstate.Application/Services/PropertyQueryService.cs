using RealEstate.Application.DTOs;
using RealEstate.Application.Mapping;
using RealEstate.Application.Queries;
using RealEstate.Domain.Abstractions;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services;

public interface IPropertyQueryService
{
    Task<(IReadOnlyList<PropertyDto> Items, long Total)> SearchAsync(
        PropertySearchQuery query, CancellationToken ct = default);
}

public sealed class PropertyQueryService(IPropertyReadRepository repo) : IPropertyQueryService
{
    public async Task<(IReadOnlyList<PropertyDto> Items, long Total)> SearchAsync(
        PropertySearchQuery query, CancellationToken ct = default)
    {
        var criteria = query.ToCriteria();
        var (entities, total) = await repo.SearchAsync(criteria, ct);
        var dtos = entities.Select(e => e.ToDto()).ToList();
        return (dtos, total);
    }
}
