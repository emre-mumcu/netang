using Backend.AppLib.Contracts;
using Backend.AppLib.Services;

namespace Backend.App_Lib.Configuration.Services;

public static class _Init
{
    public static IServiceCollection _InitServices(this IServiceCollection services) 
    {
        services.AddControllers();

        services.AddScoped<ITokenService, TokenService>();

        services._AddAuthentication();

        return services;
    }

    public static IApplicationBuilder _UseServices(this WebApplication app)
    {
        app.UseHsts();

        app.UseHttpsRedirection();

        app._UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        return app;
    }
}


/*
public static class _Init
{
    public static IServiceCollection _InitServices(this IServiceCollection services) 
    {
        return services;
    }

    public static IApplicationBuilder _UseServices(this WebApplication app)
    {
        return app;
    }
}

*/