using Backend.App_Lib.Configuration.Extensions;
using log4net;
using log4net.Config;

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-8.0

try
{
    var builder = await WebApplication.CreateBuilder(args)._ConfigureServicesAsync();
    var app = await builder.Build()._ConfigureAsync();
    // Get log4net from IoC
    app.Services.GetRequiredService<ILogger<Program>>().LogInformation("Application is starting...");
    app.Run();
}
catch (Exception ex)
{    
    {   // Manually configuring log4net
        XmlConfigurator.Configure(LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly()), new FileInfo("log4net.config"));
        LogManager.GetLogger(typeof(Program)).Error("Error in Application...", ex);
    }

    Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => { services.AddControllers(); })
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.Configure((ctx, app) =>
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsJsonAsync(Backend.App_Lib.Common.ApiResponses.Exception(ex));
            });
        });
    }).Build().Run();
}