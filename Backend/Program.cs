using Backend.App_Lib.Configuration.Extensions;

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-8.0

try
{
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
                await context.Response.WriteAsJsonAsync(Backend.App_Lib.ApiResponses.Exception(ex));
            });
        });
    }).Build().Run();
}