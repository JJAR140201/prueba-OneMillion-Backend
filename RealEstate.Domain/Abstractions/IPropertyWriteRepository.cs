using RealEstate.Domain.Entities;

namespace RealEstate.Domain.Abstractions;

public interface IPropertyWriteRepository
{
    Task<Property> CreateAsync(Property property, CancellationToken ct = default);
    Task<Property?> UpdateAsync(string id, Property property, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    Task<Property?> GetByIdAsync(string id, CancellationToken ct = default);
    Task<bool> ExistsAsync(string id, CancellationToken ct = default);
}