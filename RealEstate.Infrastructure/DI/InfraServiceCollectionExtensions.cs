using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstate.Domain.Abstractions;
using RealEstate.Infrastructure.Mongo;

namespace RealEstate.Infrastructure.DI;

public static class InfraServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var section = config.GetSection("Mongo");
        var settings = section.Get<MongoSettings>() ?? throw new InvalidOperationException("Mongo settings missing");
        services.AddSingleton(settings);
        services.AddSingleton<MongoContext>();
        services.AddScoped<IPropertyReadRepository, PropertyReadRepository>();
        services.AddScoped<IPropertyWriteRepository, PropertyWriteRepository>();
        return services;
    }
}
