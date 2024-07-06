using log4net;
using log4net.Config;
using log4net.Repository;

namespace Backend.App_Lib.Configuration.Services;

public static class _Log4Net
{
    public static WebApplicationBuilder _AddLog4Net(this WebApplicationBuilder builder)
    {
        // ILoggerRepository? logRepository = LogManager.GetRepository(System.Reflection.Assembly.GetEntryAssembly());

        // XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        // builder.Logging.ClearProviders();
        builder.Logging.AddLog4Net("log4net.config");

        return builder;
    }
}
