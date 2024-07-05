using System.Text.Json;
using Backend;
using Backend.App_Lib.Configuration.Extensions;

try
{
throw new Exception("hehe");

    var builder = await WebApplication.CreateBuilder(args)._ConfigureServicesAsync();

    var app = await builder.Build()._ConfigureAsync();

    app.Run();
}
catch (Exception ex)
{
    Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddControllers(); })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure((ctx, app) =>
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsJsonAsync(ApiResponses.Exception(ex));
            });
        });
    }).Build().Run();
}