using Backend.AppLib.Contracts;
using Backend.AppLib.Services;

namespace Backend.App_Lib.Configuration.Services;

public static class _AppServices
{
    public static IServiceCollection _AddAppServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        
        return services;
    }

    public static IApplicationBuilder _UseAppServices(this WebApplication app)
    {
        return app;
    }
}
