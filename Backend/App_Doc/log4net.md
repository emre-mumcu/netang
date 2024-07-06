# Manually Loading Configuration File

dotnet add package log4net

```cs
// Programcs
var builder = await WebApplication.CreateBuilder(args);

var logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());
XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

var app = await builder.Build();

ILog log = LogManager.GetLogger(typeof(Startup));
log.Info("Application is starting up...");

// Controller
public class MyController : ControllerBase
{
    private static readonly ILog log = LogManager.GetLogger(typeof(WeatherForecastController));
}
```

# Configure log4net as a logging provider extension

dotnet add package Microsoft.Extensions.Logging.Log4Net.AspNetCore

```cs
// Program.cs
var builder = WebApplication.CreateBuilder(args);

// builder.Logging.ClearProviders();
builder.Logging.AddLog4Net("log4net.config");

var app = builder.Build();

app.Services.GetRequiredService<ILogger<Program>>().LogInformation("Application is starting...");


// Controller

public class MyController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public MyController(ILogger<MyController> logger)
    {
        _logger = logger;
    }
}

```