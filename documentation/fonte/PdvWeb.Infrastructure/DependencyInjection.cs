using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace PdvWeb.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<AuditoriaInterceptor>();
        services.AddDbContext<PdvDbContext>((sp, opt) => opt
            .UseSqlServer(connectionString)
            .AddInterceptors(sp.GetRequiredService<AuditoriaInterceptor>()));
        return services;
    }
}
