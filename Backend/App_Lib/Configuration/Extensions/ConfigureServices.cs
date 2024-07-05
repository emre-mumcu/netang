using Backend.App_Lib.Configuration.Services;

namespace Backend.App_Lib.Configuration.Extensions;

public static class ConfigureServices
{
    public async static Task<WebApplicationBuilder> _ConfigureServicesAsync(this WebApplicationBuilder builder)
    {
        builder.Services._InitServices();

        return builder;
    }
}
