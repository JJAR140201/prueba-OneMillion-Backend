namespace RealEstate.Application.DTOs;

public sealed record PropertyDto(
    string Id,
    string IdOwner,
    string Name,
    string Address,
    decimal Price,
    string ImageUrl
);
