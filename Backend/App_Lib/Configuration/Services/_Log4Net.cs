using log4net;
using log4net.Config;

namespace Backend.App_Lib.Configuration.Services;

public static class _Log4Net
{
    public static IServiceCollection _AddLog4Net(this IServiceCollection services)
    {
        XmlConfigurator.Configure(new FileInfo("log4net.config"));

        services.AddSingleton(LogManager.GetLogger(typeof(Program)));

        return services;
    }
}
