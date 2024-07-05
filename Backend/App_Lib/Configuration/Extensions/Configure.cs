using Backend.App_Lib.Configuration;
using Backend.App_Lib.Configuration.Services;

namespace Backend.App_Lib.Configuration.Extensions;

// // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0
public static class Configure
{    
    public async static Task<WebApplication> _ConfigureAsync(this WebApplication app)
    {
        AppConfig.Instance.WebHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();

        app._UseServices();

        return app;
    }
}
