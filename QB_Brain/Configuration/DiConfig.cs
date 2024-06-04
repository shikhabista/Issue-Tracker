using Base.Configuration;
using Microsoft.EntityFrameworkCore;
using QB_Web.Generator;
using QB_Web.Helpers;

namespace QB_Web.Configuration;

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