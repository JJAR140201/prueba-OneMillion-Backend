using RealEstate.Application.DTOs;
using RealEstate.Domain.Entities;
using MongoDB.Bson;

namespace RealEstate.Application.Mapping;

public static class PropertyMapping
{
    public static PropertyDto ToDto(this Property p) =>
        new(p.Id, p.IdOwner, p.Name, p.Address, p.Price, p.ImageUrl);

    public static Property ToEntity(this CreatePropertyDto dto) =>
        new()
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = dto.Name,
            Address = dto.Address,
            Price = dto.Price,
            IdOwner = dto.IdOwner,
            CodeInternal = dto.CodeInternal,
            Year = dto.Year
        };

    public static Property ToEntity(this UpdatePropertyDto dto, string id) =>
        new()
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            Price = dto.Price,
            IdOwner = dto.IdOwner,
            CodeInternal = dto.CodeInternal,
            Year = dto.Year
        };
}
