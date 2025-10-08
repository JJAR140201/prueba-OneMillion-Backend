using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs;
using RealEstate.Application.Queries;
using RealEstate.Application.Services;

namespace RealEstate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class PropertiesController(
    IPropertyQueryService queryService,
    IPropertyCommandService commandService) : ControllerBase
{
    /// <summary>
    /// Obtiene propiedades con filtros avanzados y paginación
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<PropertyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProperties(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] string? idOwner,
        [FromQuery] string? codeInternal,
        [FromQuery] int? year,
        [FromQuery] int? minYear,
        [FromQuery] int? maxYear,
        [FromQuery] string? sortBy = "Price",
        [FromQuery] string? sortOrder = "desc",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken ct = default)
    {
        var query = new PropertySearchQuery(
            name, address, minPrice, maxPrice, idOwner, codeInternal, 
            year, minYear, maxYear, sortBy, sortOrder, page, pageSize);
            
        var (items, total) = await queryService.SearchAsync(query, ct);
        return Ok(new PagedResult<PropertyDto>(items, total, page, pageSize));
    }

    /// <summary>
    /// Obtiene una propiedad por su ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPropertyById(string id, CancellationToken ct = default)
    {
        var property = await commandService.GetByIdAsync(id, ct);
        return property is not null ? Ok(property) : NotFound();
    }

    /// <summary>
    /// Crea una nueva propiedad
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProperty(
        [FromBody] CreatePropertyDto dto, 
        CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await commandService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetPropertyById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Actualiza una propiedad existente
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(PropertyDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProperty(
        string id, 
        [FromBody] UpdatePropertyDto dto, 
        CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await commandService.UpdateAsync(id, dto, ct);
        return updated is not null ? Ok(updated) : NotFound();
    }

    /// <summary>
    /// Elimina una propiedad
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProperty(string id, CancellationToken ct = default)
    {
        var deleted = await commandService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }

    /// <summary>
    /// Lista todas las propiedades sin filtros (útil para obtener todo el catálogo)
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(PagedResult<PropertyDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProperties(
        [FromQuery] string? sortBy = "Price",
        [FromQuery] string? sortOrder = "desc",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 50,
        CancellationToken ct = default)
    {
        var query = new PropertySearchQuery(
            SortBy: sortBy, SortOrder: sortOrder, Page: page, PageSize: pageSize);
            
        var (items, total) = await queryService.SearchAsync(query, ct);
        return Ok(new PagedResult<PropertyDto>(items, total, page, pageSize));
    }

    /// <summary>
    /// [TEMPORAL] Limpia toda la colección de propiedades 
    /// </summary>
    [HttpDelete("dev/clear-all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> ClearAllProperties(CancellationToken ct = default)
    {
        await commandService.ClearAllAsync(ct);
        return Ok(new { Message = "Todas las propiedades han sido eliminadas" });
    }
}

/// <summary>Envelope simple de paginación</summary>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, long Total, int Page, int PageSize);
