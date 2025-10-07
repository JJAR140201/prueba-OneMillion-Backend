using RealEstate.Api.DI;
using RealEstate.Infrastructure.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "RealEstate API",
        Version = "v1",
        Description = "API para gestión de propiedades inmobiliarias"
    });
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddApiServices();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Habilitar CORS
app.UseCors();

// Habilitar Swagger en todos los entornos para desarrollo
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RealEstate API V1");
    c.RoutePrefix = "swagger"; // Esto hace que Swagger esté disponible en /swagger
});

app.MapControllers();
app.Run();
