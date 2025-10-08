using System.ComponentModel.DataAnnotations;

namespace RealEstate.Application.DTOs;

public sealed record CreatePropertyDto(
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    string Name,
    
    [Required(ErrorMessage = "La dirección es requerida")]
    [StringLength(500, ErrorMessage = "La dirección no puede exceder 500 caracteres")]
    string Address,
    
    [Required(ErrorMessage = "El precio es requerido")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    decimal Price,
    
    [Required(ErrorMessage = "El ID del propietario es requerido")]
    string IdOwner,
    
    [StringLength(50, ErrorMessage = "El código interno no puede exceder 50 caracteres")]
    string? CodeInternal = null,
    
    [Range(1800, 2100, ErrorMessage = "El año debe estar entre 1800 y 2100")]
    int? Year = null
);

public sealed record UpdatePropertyDto(
    [Required(ErrorMessage = "El nombre es requerido")]
    [StringLength(200, ErrorMessage = "El nombre no puede exceder 200 caracteres")]
    string Name,
    
    [Required(ErrorMessage = "La dirección es requerida")]
    [StringLength(500, ErrorMessage = "La dirección no puede exceder 500 caracteres")]
    string Address,
    
    [Required(ErrorMessage = "El precio es requerido")]
    [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
    decimal Price,
    
    [Required(ErrorMessage = "El ID del propietario es requerido")]
    string IdOwner,
    
    [StringLength(50, ErrorMessage = "El código interno no puede exceder 50 caracteres")]
    string? CodeInternal = null,
    
    [Range(1800, 2100, ErrorMessage = "El año debe estar entre 1800 y 2100")]
    int? Year = null
);