using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Queries;
using RealEstate.Application.Services;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PropertiesController(IPropertyQueryService service) : ControllerBase
{
    /// <summary>
    /// Obtiene propiedades filtrando por name, address y rango de price.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<PropertyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Get(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var query = new PropertySearchQuery(name, address, minPrice, maxPrice, page, pageSize);
        var (items, total) = await service.SearchAsync(query, ct);
        return Ok(new PagedResult<PropertyDto>(items, total, page, pageSize));
    }
}

/// <summary>Envelope simple de paginación</summary>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, long Total, int Page, int PageSize);
