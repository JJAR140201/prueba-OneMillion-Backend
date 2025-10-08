using RealEstate.Application.DTOs;
using RealEstate.Application.Mapping;
using RealEstate.Domain.Abstractions;

namespace RealEstate.Application.Services;

public interface IPropertyCommandService
{
    Task<PropertyDto> CreateAsync(CreatePropertyDto dto, CancellationToken ct = default);
    Task<PropertyDto?> UpdateAsync(string id, UpdatePropertyDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(string id, CancellationToken ct = default);
    Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default);
    Task ClearAllAsync(CancellationToken ct = default);
}

public sealed class PropertyCommandService(
    IPropertyWriteRepository writeRepo) : IPropertyCommandService
{
    public async Task<PropertyDto> CreateAsync(CreatePropertyDto dto, CancellationToken ct = default)
    {
        var entity = dto.ToEntity();
        var created = await writeRepo.CreateAsync(entity, ct);
        return created.ToDto();
    }

    public async Task<PropertyDto?> UpdateAsync(string id, UpdatePropertyDto dto, CancellationToken ct = default)
    {
        var existing = await writeRepo.GetByIdAsync(id, ct);
        if (existing is null) return null;

        var updated = dto.ToEntity(id);
        var result = await writeRepo.UpdateAsync(updated, ct);
        return result ? PropertyMapping.ToDto(updated) : null;
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken ct = default)
    {
        return await writeRepo.DeleteAsync(id, ct);
    }

    public async Task<PropertyDto?> GetByIdAsync(string id, CancellationToken ct = default)
    {
        var entity = await writeRepo.GetByIdAsync(id, ct);
        return entity?.ToDto();
    }

    public async Task ClearAllAsync(CancellationToken ct = default)
    {
        await writeRepo.ClearAllAsync(ct);
    }
}