using Microsoft.Extensions.DependencyInjection;
using RealEstate.Application.Services;

namespace RealEstate.Api.DI;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IPropertyQueryService, PropertyQueryService>();
        return services;
    }
}
