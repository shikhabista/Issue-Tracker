using Base.Configuration;
using IT_Web.Generator;
using IT_Web.Helpers;
using Microsoft.EntityFrameworkCore;

namespace IT_Web.Configuration;

public static class DiConfig
{
    public static IServiceCollection ConfigureAppDi(this IServiceCollection services)
    {
        return services
            .AddScoped<DbContext, AppDbContext>()
            .AddTransient<IJwtTokenGenerator, JwtTokenGenerator>()
            .ConfigureBase()
            // .ConfigureQb()
            .ConfigureMisc();
    }

    private static IServiceCollection ConfigureMisc(this IServiceCollection services)
    {
        return services.AddTransient<INotificationHelper, NotificationHelper>();
    }
    
}