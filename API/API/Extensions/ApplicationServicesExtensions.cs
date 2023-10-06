using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add scoped services
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        // Add AutoMapper for object mapping
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // Add entity dbContext for app, add postgreSQL connection for dbContext
        services.AddDbContext<MoviesContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });
        // Add CORS service for frontend client server
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy", policy =>
            {
                policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(configuration["ClientURL"]);
            });
        });
        return services;
    }
}