using IQT.Authorization.API.Data;
using IQT.Authorization.API.Services;
using Microsoft.EntityFrameworkCore;

namespace IQT.Authorization.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<AuthDbContext>(options => options
                .UseSqlServer(configuration.GetConnectionString("SQLServerCs"),
                    p => p.EnableRetryOnFailure(
                            maxRetryCount: 1,
                            maxRetryDelay: TimeSpan.FromSeconds(5),
                            errorNumbersToAdd: null).
                            MigrationsHistoryTable("EFMigrations"))
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging());

        services.AddScoped<AuthService>();

        services.AddTransient<UserInitializerService>();

        return services;
    }
}
